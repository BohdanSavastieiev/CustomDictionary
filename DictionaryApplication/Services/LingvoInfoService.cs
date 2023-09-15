using DictionaryApplication.Clients;
using DictionaryApplication.DTOs;
using DictionaryApplication.Mappers;
using DictionaryApplication.Models.DbModels;

namespace DictionaryApplication.Services
{
    public class LingvoInfoService : ILingvoInfoService
    {
        private readonly LingvoInfoApiClient _lingvoInfoClient;
        private readonly ILingvoInfoMapper _lingvoInfoMapper;
        public LingvoInfoService(
            LingvoInfoApiClient lingvoInfoClient,
            ILingvoInfoMapper lingvoInfoMapper)
        {
            _lingvoInfoClient = lingvoInfoClient;
            _lingvoInfoMapper = lingvoInfoMapper;
        }
        public async Task<LexemeInputDto> GetLingvoInfoAsync(string text, string srcLang, string dstLang, bool includeSound)
        {
            var dto = await _lingvoInfoClient.GetLingvoInfoAsync(text, srcLang, dstLang, includeSound);
            if (dto != null)
            {
                return _lingvoInfoMapper.MapToLexemeInput(dto);

            }

            throw new BadHttpRequestException("The lingvo information has not been successfully received.");
        }
    }
}
