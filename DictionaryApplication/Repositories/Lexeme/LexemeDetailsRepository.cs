using DictionaryApplication.Data;
using DictionaryApplication.DTOs;
using DictionaryApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DictionaryApplication.Repositories
{
    public class LexemeDetailsRepository : ILexemeDetailsRepository
    {
        private readonly ApplicationDbContext _context;

        public LexemeDetailsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        private static void CheckLexeme(Lexeme? lexeme)
        {
            if (lexeme == null)
            {
                throw new ArgumentException("Lexeme with Id specified was not found.");
            }
        }
        public async Task<LexemeDetailsDto?> GetByIdAsync(int id)
        {
            var lexeme = await _context.Lexemes.FirstOrDefaultAsync(x => x.Id == id);
            CheckLexeme(lexeme);

            var result = new LexemeDetailsDto(_context, lexeme.Id);
            return result;
        }

        public async Task<List<LexemeDetailsDto>> GetAllAsync(params int[] userDictionaryIds)
        {
            List<int> studiedLexemeIds = await _context.Lexemes.Where(x => userDictionaryIds.Contains(x.DictionaryId)
                && (x.LexemePairs != null && x.LexemePairs.Count > 0 || x.LexemeInformations.Any()))
                .Select(x => x.Id).ToListAsync();
            var result = new List<LexemeDetailsDto>();
            foreach (var id in studiedLexemeIds)
            {
                result.Add(new LexemeDetailsDto(_context, id));
            }
            return result;
        }
        public async Task<(List<LexemeDetailsDto>, int)> GetAllFilterAsync(int skip, int take, params int[] userDictionaryIds)
        {
            List<LexemeDetailsDto> all = await GetAllAsync(userDictionaryIds);
            return await GetFilteredForPagingAsync(all, skip, take);
        }

        public async Task<(List<LexemeDetailsDto>, int)> GetAllFilterAsync(List<LexemeDetailsDto> lexemeDetails, int skip, int take)
        {
            return await GetFilteredForPagingAsync(lexemeDetails, skip, take);
        }

        private static async Task<(List<LexemeDetailsDto>, int)> GetFilteredForPagingAsync(List<LexemeDetailsDto> lexemeDetails, int skip, int take)
        {
            List<LexemeDetailsDto> relevant = lexemeDetails.Skip(skip).Take(take).ToList();
            var total = lexemeDetails.Count;

            (List<LexemeDetailsDto>, int) result = (relevant, total);

            return result;
        }
    }
}
