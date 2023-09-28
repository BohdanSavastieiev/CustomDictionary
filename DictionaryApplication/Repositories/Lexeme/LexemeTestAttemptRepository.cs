using AutoMapper;
using DictionaryApplication.Data;
using DictionaryApplication.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DictionaryApplication.Repositories
{
    public class LexemeTestAttemptRepository : ILexemeTestAttemptRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public LexemeTestAttemptRepository(
            ApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<LexemeTestAttemptDto>> GetAllAsync(params int[] userDictionaryIds)
        {
            var result = new List<LexemeTestAttemptDto>();
            List<int> studiedLexemeIds = await _context.Lexemes
                .Where(x => userDictionaryIds.Contains(x.DictionaryId))
                .Select(x => x.Id)
                .ToListAsync();

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

        public async Task<LexemeTestAttemptDto?> GetByIdAsync(int lexemeId)
        {
            var lexeme = await _context.Lexemes
                .Include(x => x.LexemeInformations)
                .Include(x => x.WordForms)
                .FirstOrDefaultAsync(x => x.Id == lexemeId)
                ?? throw new ArgumentNullException(nameof(lexemeId), "The lexeme for update was not found.");

            var result = new LexemeTestAttemptDto
            {
                Lexeme = _mapper.Map<LexemeDto>(lexeme)
            };

            return result;
        }

        public async Task<int> GetLexemesInDictionariesCount(params int[] userDictionaryIds)
        {
            List<int> studiedLexemeIds = await _context.Lexemes
                .Where(x => userDictionaryIds.Contains(x.DictionaryId))
                .Select(x => x.Id)
                .ToListAsync();

            return studiedLexemeIds.Count;
        }

        public async Task UpdateTestResultAsync(LexemeTestAttemptDto lexemeTest)
        {
            var lexemeToUpdate = await _context.Lexemes.FindAsync(lexemeTest.Lexeme.Id)
                ?? throw new ArgumentNullException(nameof(lexemeTest), "The lexeme for update was not found.");

            lexemeToUpdate.CorrectTestAttempts = lexemeTest.Lexeme.CorrectTestAttempts;
            lexemeToUpdate.TotalTestAttempts = lexemeTest.Lexeme.TotalTestAttempts;

            _context.Update(lexemeToUpdate);
            await _context.SaveChangesAsync();
        }

    }
}
