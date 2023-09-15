using DictionaryApplication.DTOs;

namespace DictionaryApplication.Services
{
    public interface ILingvoInfoService
    {
        Task<LexemeInputDto> GetLingvoInfoAsync(string text, string srcLang, string dstLang, bool includeSound);
    }
}
