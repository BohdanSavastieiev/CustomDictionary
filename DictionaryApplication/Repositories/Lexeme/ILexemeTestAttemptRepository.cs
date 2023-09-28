using DictionaryApplication.DTOs;

namespace DictionaryApplication.Repositories
{
    public interface ILexemeTestAttemptRepository
    {
        Task<LexemeTestAttemptDto?> GetByIdAsync(int lexemeId);
        Task<List<LexemeTestAttemptDto>> GetAllAsync(params int[] userDictionaryIds);
        Task UpdateTestResultAsync(LexemeTestAttemptDto lexemeTest);
        Task<int> GetLexemesInDictionariesCount(params int[] userDictionaryIds);
    }
}
