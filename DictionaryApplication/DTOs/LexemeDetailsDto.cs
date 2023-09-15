using DictionaryApplication.Data;
using DictionaryApplication.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace DictionaryApplication.DTOs
{
    public class LexemeDetailsDto
    {
        public Lexeme Lexeme { get; set; }
        public string? TranslationsRepresentation { get; set; }
        public double TestResults { get; set; }
        public string TestResultsRepresentation { get; set; }

        public LexemeDetailsDto(ApplicationDbContext context, int lexemeId)
        {
            var lexeme = context.Lexemes
                .Include(x => x.LexemeInformations)
                    .ThenInclude(x => x.Examples)
                .Include(x => x.LexemeInformations)
                    .ThenInclude(x => x.RelatedLexemes)
                .Include(x => x.WordForms)
                .FirstOrDefault(x => x.Id == lexemeId);

            if (lexeme == null)
            {
                throw new ArgumentException("Lexeme was not found.");
            }
            Lexeme = lexeme;

            TranslationsRepresentation = lexeme.LexemeInformations.Count > 1
                ? string.Join(Environment.NewLine, lexeme.LexemeInformations.Select(x => x.Translation).Select((s, i) => $"{i + 1}. {s}"))
                : lexeme.LexemeInformations.Select(x => x.Translation).FirstOrDefault();

            if (TranslationsRepresentation == null)
            {
                var Translations = context.Lexemes.Where(x =>
                        context.LexemeTranslationPairs
                            .Where(y => y.LexemeId == Lexeme.Id)
                            .Select(y => y.TranslationId).Contains(x.Id))
                        .ToList();

                TranslationsRepresentation = Translations.Count > 1
                    ? string.Join(Environment.NewLine, Translations.Select(x => x.Word).Select((s, i) => $"{i + 1}. {s}"))
                    : Translations.Select(x => x.Word).FirstOrDefault();

                if (TranslationsRepresentation == null)
                {
                    TranslationsRepresentation = string.Empty;
                }
            }

            if (Lexeme.TotalTestAttempts > 0)
            {
                TestResults = (double)Lexeme.CorrectTestAttempts / Lexeme.TotalTestAttempts;
            }
            else
            {
                TestResults = 0;
            }

            if (Lexeme.TotalTestAttempts == 0)
            {
                TestResultsRepresentation = "0 attempts";
            }
            else
            {
                string results = string.Format("{0,6:##0.00; }", TestResults * 100);
                TestResultsRepresentation = $"{results} % — {Lexeme.CorrectTestAttempts} out of {Lexeme.TotalTestAttempts}";
            }
        }
    }
}
