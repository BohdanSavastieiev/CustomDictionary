using DictionaryApplication.DTOs;

namespace DictionaryApplication.Repositories
{
    public interface ILexemeInputRepository
    {
        Task CreateAsync(int dictionaryId, LexemeInputDto lexemeInput);
        Task UpdateAsync(int lexemeId, LexemeInputDto lexemeInput);
        Task DeleteAsync(int lexemeId);
        Task<LexemeInputDto?> GetByIdAsync(int lexemeId);
        Task<List<LexemeInputDto>> GetAllAsync(params int[] userDictionaryIds);

    }
}
