using DictionaryApplication.Data;
using DictionaryApplication.DTOs;
using DictionaryApplication.Models;
using DictionaryApplication.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Packaging.Signing;
using System.Linq;
using System.Text.RegularExpressions;

namespace DictionaryApplication.Services
{
    public class KnowledgeTestService
    {
        private readonly ILexemeTestAttemptRepository _lexemeTestAttemptRepository;
        public KnowledgeTestService(ILexemeTestAttemptRepository lexemeTestAttemptRepository)
        {
            _lexemeTestAttemptRepository = lexemeTestAttemptRepository;
        }
        public async Task<bool> ContainAnyLexemesAsync(IEnumerable<int> userDictionaryIds)
        {
            var lexemes = await _lexemeTestAttemptRepository.GetAllAsync(userDictionaryIds.ToArray());
            return lexemes.Any();
        }
        public async Task<int> GetTotalLexemesAmount(IEnumerable<int> userDictionaryIds)
        {
            var result = await _lexemeTestAttemptRepository.GetLexemesInDictionariesCount(userDictionaryIds.ToArray());
            return result;
        }
        public async Task<bool> AreParametersValid(KnowledgeTestParameters testParameters)
        {
            return testParameters != null
                && testParameters.SelectedDictionaryIds != null
                && testParameters.SelectedDictionaryIds.Count > 0
                && await ContainAnyLexemesAsync(testParameters.SelectedDictionaryIds)
                && testParameters.NumberOfWords > 0;
        }
        public async Task<List<LexemeTestAttemptDto>> GetLexemeTestAttemptsAsync(KnowledgeTestParameters testParameters)
        {
            Random random = new Random();
            var lexemeTestAttempts = await _lexemeTestAttemptRepository.GetAllAsync(testParameters.SelectedDictionaryIds.ToArray());

            foreach (var lexemeTestAttempt in lexemeTestAttempts)
            {
                var translationsRepresentation = string.Join(Environment.NewLine, 
                    lexemeTestAttempt.Lexeme.LexemeInformations.Select(l => l.Translation).Select(s => $"• {s}"));

                if (testParameters.IsUserTranslatesStudiedLanguage)
                {
                    lexemeTestAttempt.LexemeTestRepresentation = lexemeTestAttempt.Lexeme.Word;
                    lexemeTestAttempt.CorrectAnswerRepresentation = translationsRepresentation;
                }
                else
                {
                    lexemeTestAttempt.LexemeTestRepresentation = translationsRepresentation;
                    lexemeTestAttempt.CorrectAnswerRepresentation = lexemeTestAttempt.Lexeme.Word;
                }
            }

            switch (testParameters.TestType)
            {
                case TestType.AllWords:
                    return lexemeTestAttempts.OrderBy(l => random.Next())
                        .Take(testParameters.NumberOfWords)
                        .ToList();
                case TestType.LastWords:
                    return lexemeTestAttempts.TakeLast(testParameters.NumberOfWords)
                        .ToList();
                case TestType.WordsWithWorstResults:
                    return lexemeTestAttempts.OrderBy(x =>
                        x.Lexeme.TotalTestAttempts == 0
                        ? random.Next()
                        : x.Lexeme.CorrectTestAttempts / x.Lexeme.TotalTestAttempts)
                        .ThenBy(x => random.Next())
                        .Take(testParameters.NumberOfWords)
                        .ToList();
                default:
                    return lexemeTestAttempts;
            }
        }
        public void CheckAnswers(List<LexemeTestAttemptDto> lexemes, KnowledgeTestParameters knowledgeTestParameters)
        {
            foreach (var lexeme in lexemes)
            {
                if (lexeme.TestAnswer == null)
                {
                    continue;
                }

                string cleanedLine = Regex.Replace(lexeme.LexemeTestRepresentation, @"\d+\.", "").Trim();
                string[] correctTranslationsIfStudiedLanguage = lexeme.Lexeme.LexemeInformations
                    .SelectMany(x => Regex.Split(x.Translation, @"[;,]\s*")).ToArray();

                var correctTranslationsIfTranslatedLanguage = lexeme.Lexeme.WordForms.Any()
                        ? lexeme.Lexeme.WordForms.Select(x => x.Word).ToArray()
                        : new[] { lexeme.Lexeme.Word };

                lexeme.IsCorrectAnswer = knowledgeTestParameters.IsUserTranslatesStudiedLanguage
                ? IsCorrectAnswer(lexeme.TestAnswer, correctTranslationsIfStudiedLanguage)
                : IsCorrectAnswer(lexeme.TestAnswer, correctTranslationsIfTranslatedLanguage);
            }
        }
        public bool IsCorrectAnswer(string userAnswer, params string[] correctTranslations)
        {
            var userAnswers = userAnswer.ToLower().Trim().Split(", ");

            for (int i = 0; i < correctTranslations.Length; i++)
            {
                foreach (var answer in userAnswers)
                {
                    if (correctTranslations[i].Equals(answer, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                    var levensteinDistance = LevenshteinDistance(correctTranslations[i], answer);
                    if (levensteinDistance <= 1)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        private int LevenshteinDistance(string s1, string s2)
        {
            int[,] d = new int[s1.Length + 1, s2.Length + 1];

            for (int i = 0; i <= s1.Length; i++)
            {
                d[i, 0] = i;
            }

            for (int j = 0; j <= s2.Length; j++)
            {
                d[0, j] = j;
            }

            for (int j = 1; j <= s2.Length; j++)
            {
                for (int i = 1; i <= s1.Length; i++)
                {
                    if (s1[i - 1] == s2[j - 1])
                    {
                        d[i, j] = d[i - 1, j - 1];
                    }
                    else
                    {
                        d[i, j] = Math.Min(d[i - 1, j], Math.Min(d[i, j - 1], d[i - 1, j - 1])) + 1;
                    }
                }
            }

            return d[s1.Length, s2.Length];
        }
        public async Task SetResults(List<LexemeTestAttemptDto> lexemes)
        {
            foreach (var lexeme in lexemes)
            {
                lexeme.Lexeme.TotalTestAttempts++;

                if (lexeme.IsCorrectAnswer)
                {
                    lexeme.Lexeme.CorrectTestAttempts++;
                }

                await _lexemeTestAttemptRepository.UpdateTestResultAsync(lexeme);
            }
        }
    }
}
