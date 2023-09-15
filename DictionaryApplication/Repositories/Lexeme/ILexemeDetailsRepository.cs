using DictionaryApplication.DTOs;

namespace DictionaryApplication.Repositories
{
    public interface ILexemeDetailsRepository
    {
        Task<LexemeDetailsDto?> GetByIdAsync(int id);
        Task<List<LexemeDetailsDto>> GetAllAsync(params int[] userDictionaryIds);
        Task<(List<LexemeDetailsDto>, int)> GetAllFilterAsync(int skip, int take, params int[] userDictionaryIds);
        Task<(List<LexemeDetailsDto>, int)> GetAllFilterAsync(List<LexemeDetailsDto> lexemeDetails, int skip, int take);
    }
}
