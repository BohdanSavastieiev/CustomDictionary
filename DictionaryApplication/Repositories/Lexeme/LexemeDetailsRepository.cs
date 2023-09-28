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
            var lexeme = await _context.Lexemes
                .Include(x => x.WordForms)
                .Include(x => x.LexemeInformations)
                    .ThenInclude(x => x.Examples)
                .Include(x => x.LexemeInformations)
                    .ThenInclude(x => x.RelatedLexemes)
                .FirstOrDefaultAsync(x => x.Id == id);
            CheckLexeme(lexeme);

            var result = new LexemeDetailsDto(lexeme);
            return result;
        }

        public async Task<List<LexemeDetailsDto>> GetAllAsync(params int[] userDictionaryIds)
        {
            List<int> studiedLexemeIds = await _context.Lexemes
                .Where(x => userDictionaryIds.Contains(x.DictionaryId)
                    && x.LexemeInformations.Any())
                .Select(x => x.Id).ToListAsync();

            var result = new List<LexemeDetailsDto>();
            foreach (var id in studiedLexemeIds)
            {
                var lexemeDetails = await GetByIdAsync(id);
                result.Add(lexemeDetails);
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
