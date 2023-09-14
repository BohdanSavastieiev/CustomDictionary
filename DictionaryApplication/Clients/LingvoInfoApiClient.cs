using DictionaryApplication.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using System.Net;

namespace DictionaryApplication.Clients
{
    public class LingvoInfoApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _lingvoInfoEndpoint;
        public LingvoInfoApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("LingvoInfoApi");
        }

        public async Task<LingvoInfoDto?> GetLingvoInfoAsync(string text, string srcLang, string dstLang, bool includeSound)
        {
            string requestUrl = $"{_lingvoInfoEndpoint}?text={WebUtility.UrlEncode(text)}&srcLang={WebUtility.UrlEncode(srcLang)}&dstLang={WebUtility.UrlEncode(dstLang)}&includeSound={includeSound}";
            var response = await _httpClient.GetAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var dto = JsonConvert.DeserializeObject<LingvoInfoDto>(content);
                return dto;
            }
            else
            {
                return null;
            }
        }
    }
}
