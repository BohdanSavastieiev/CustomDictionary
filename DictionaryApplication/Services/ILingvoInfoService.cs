using DictionaryApplication.DTOs;
using DictionaryApplication.Models;

namespace DictionaryApplication.Services
{
    public interface ILingvoInfoService
    {
        Task<LexemeInput> GetLingvoInfoAsync(string text, string srcLang, string dstLang, bool includeSound);
    }
}
