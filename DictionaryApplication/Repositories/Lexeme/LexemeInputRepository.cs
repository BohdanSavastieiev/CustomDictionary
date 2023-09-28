using DictionaryApplication.Data;
using DictionaryApplication.DTOs;
using DictionaryApplication.Models;
using DictionaryApplication.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DictionaryApplication.Repositories
{
    public class LexemeInputRepository : ILexemeInputRepository
    {
        private readonly ApplicationDbContext _context;
        public LexemeInputRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(int dictionaryId, LexemeInputDto lexemeInput)
        {
            var userDictionary = await _context.UserDictionaries.FirstOrDefaultAsync(x => x.Id == dictionaryId)
                ?? throw new ArgumentNullException(nameof(dictionaryId), "The dictionary for update was not found.");

            var lexeme = new Lexeme
            {
                DictionaryId = dictionaryId,
                Word = lexemeInput.Lexeme,
                Transcription = lexemeInput.Transcription
            };
            await _context.Lexemes.AddAsync(lexeme);

            foreach (var wordForm in lexemeInput.WordForms)
            {
                var form = new Models.DbModels.WordForm
                {
                    Lexeme = lexeme,
                    Word = wordForm.Word
                };

                await _context.WordForms.AddAsync(form);
            }

            foreach (var lexemeInformation in lexemeInput.LexemeInformations)
            {
                var translation = new LexemeInformation
                {
                    TranslatedLexeme = lexeme,
                    Translation = lexemeInformation.Translation
                };

                foreach(var usageExample in lexemeInformation.Examples)
                {
                    var example = new UsageExample
                    {
                        LexemeInformation = translation,
                        NativeExample = usageExample.NativeExample,
                        TranslatedExample = usageExample.TranslatedExample
                    };

                    await _context.UsageExamples.AddAsync(example);
                }
                foreach (var relatedLexeme in lexemeInformation.RelatedLexemes)
                {
                    var relLexeme = new RelatedLexeme
                    {
                        LexemeInformation = translation,
                        Word = relatedLexeme.Word,
                        Type = relatedLexeme.Type.ToString()
                    };

                    await _context.RelatedLexemes.AddAsync(relLexeme);
                }

                await _context.LexemeInformations.AddAsync(translation);
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int lexemeId, LexemeInputDto lexemeInput)
        {
            var lexeme = await _context.Lexemes.FirstOrDefaultAsync(x => x.Id == lexemeId)
                ?? throw new ArgumentNullException(nameof(lexemeId), "The lexeme for update was not found.");

            lexeme.Word = lexemeInput.Lexeme;

            var userDictionary = await _context.UserDictionaries.FirstOrDefaultAsync(x => x.Id == lexeme.DictionaryId)
                ?? throw new ArgumentNullException(nameof(lexemeId), "The dictionary for update was not found.");


            // Удаление старых форм слова
            var oldWordForms = _context.WordForms.Where(x => x.LexemeId == lexemeId);
            _context.WordForms.RemoveRange(oldWordForms);

            // Добавление новых форм слова
            foreach (var wordForm in lexemeInput.WordForms)
            {
                var form = new Models.DbModels.WordForm
                {
                    Lexeme = lexeme,
                    Word = wordForm.Word
                };

                await _context.WordForms.AddAsync(form);
            }

            // Удаление старой информации о лексеме
            var oldLexemeInformations = _context.LexemeInformations.Where(x => x.TranslatedLexemeId == lexemeId);
            _context.LexemeInformations.RemoveRange(oldLexemeInformations);

            // Добавление новой информации о лексеме
            foreach (var lexemeInformation in lexemeInput.LexemeInformations)
            {
                var translation = new LexemeInformation
                {
                    TranslatedLexeme = lexeme,
                    Translation = lexemeInformation.Translation
                };

                foreach (var usageExample in lexemeInformation.Examples)
                {
                    var example = new UsageExample
                    {
                        LexemeInformation = translation,
                        NativeExample = usageExample.NativeExample,
                        TranslatedExample = usageExample.TranslatedExample
                    };

                    await _context.UsageExamples.AddAsync(example);
                }
                foreach (var relatedLexeme in lexemeInformation.RelatedLexemes)
                {
                    var relLexeme = new RelatedLexeme
                    {
                        LexemeInformation = translation,
                        Word = relatedLexeme.Word,
                        Type = relatedLexeme.Type.ToString()
                    };

                    await _context.RelatedLexemes.AddAsync(relLexeme);
                }

                await _context.LexemeInformations.AddAsync(translation);
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int lexemeId)
        {
            var lexeme = await _context.Lexemes.FirstOrDefaultAsync(x => x.Id == lexemeId)
                ?? throw new ArgumentNullException(nameof(lexemeId), "The lexeme was not found.");

            // Удаление форм слова
            var wordForms = _context.WordForms.Where(x => x.LexemeId == lexemeId);
            _context.WordForms.RemoveRange(wordForms);

            // Удаление информации о лексеме
            var lexemeInformations = _context.LexemeInformations.Where(x => x.TranslatedLexemeId == lexemeId);
            _context.LexemeInformations.RemoveRange(lexemeInformations);

            _context.Lexemes.Remove(lexeme);

            await _context.SaveChangesAsync();
        }

        public async Task<LexemeInputDto?> GetByIdAsync(int lexemeId)
        {
            var lexeme = await _context.Lexemes.FirstOrDefaultAsync(x => x.Id == lexemeId)
                ?? throw new ArgumentNullException(nameof(lexemeId), "The lexeme was not found.");

            var wordForms = _context.WordForms.Where(x => x.LexemeId == lexeme.Id)
            .Select(x => new WordFormDto
            {
                Word = x.Word,
                // ... другие свойства
            }).ToList();

            var lexemeInformations = _context.LexemeInformations.Where(x => x.TranslatedLexemeId == lexeme.Id)
            .Select(x => new LexemeInformationDto
            {
                Translation = x.Translation,
                Examples = _context.UsageExamples.Where(ue => ue.LexemeInformationId == x.Id)
                .Select(ue => new UsageExampleDto
                {
                    NativeExample = ue.NativeExample,
                    TranslatedExample = ue.TranslatedExample
                }).ToList(),
                RelatedLexemes = _context.RelatedLexemes.Where(rl => rl.LexemeInformationId == x.Id)
                .Select(rl => new RelatedLexemeDto
                {
                    Word = rl.Word,
                    Type = ConvertToRelatedLexemeType(rl.Type)
                }).ToList()
            }).ToList();

            var result = new LexemeInputDto
            {
                Lexeme = lexeme.Word,
                WordForms = wordForms,
                LexemeInformations = lexemeInformations
            };

            return result;
        }

        private static RelatedLexemeType ConvertToRelatedLexemeType(string typeString)
        {
            return typeString switch
            {
                "Synonym" => RelatedLexemeType.Synonym,
                "Antonym" => RelatedLexemeType.Antonym,
                "Derivative" => RelatedLexemeType.Derivative,
                _ => throw new InvalidOperationException($"Unknown RelatedLexemeType: {typeString}")
            };
        }


        public async Task<List<LexemeInputDto>> GetAllAsync(params int[] userDictionaryIds)
        {
            var result = new List<LexemeInputDto>();
            List<int> studiedLexemeIds = await _context.Lexemes.Where(x => userDictionaryIds.Contains(x.DictionaryId))
                .Select(x => x.Id).ToListAsync();
            foreach (var lexemeId in studiedLexemeIds)
            {
                var lexeme = await GetByIdAsync(lexemeId);
                if (lexeme != null)
                {
                    result.Add(lexeme);
                }
            }

            return result;
        }

    }
}
