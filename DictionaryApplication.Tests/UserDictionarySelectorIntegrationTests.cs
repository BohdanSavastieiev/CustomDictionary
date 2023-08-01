using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryApplication.Tests
{
    public class UserDictionarySelectorIntegrationTests : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _client;

        public UserDictionarySelectorIntegrationTests(TestingWebAppFactory<Program> factory)
            => _client = factory.CreateClient();

        [Fact]
        public async Task OnGetAsync_ReturnsPageResult_WhenUserIsAuthenticated()
        {
            var response = await _client.GetAsync("/UserDictionarySelector/Index");

            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType?.ToString());
        }

        [Fact]
        public async Task OnPostAsync_ReturnsPageResult_IfUserIsUnauthorized()
        {
            var formData = new Dictionary<string, string>
            {
                { "UserId", SeedData.CreatedUserId},
                { "Name", "Dictionary" },
                { "Description", "Test Dictionary" },
                { "StudiedLangId", SeedData.StudiedLangId.ToString()},
                { "TranslationLangId", SeedData.TranslationLangId.ToString() },

            };

            var requestBody = new FormUrlEncodedContent(formData);

            var response = await _client.PostAsync("/UserDictionarySelector/Create", requestBody);

            var temp = $"Response Content: {await response.Content.ReadAsStringAsync()}";

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task OnPostAsync_ReturnsPageResult_WhenModelStateIsInvalid()
        {
            var requestBody = new StringContent("", Encoding.UTF8, "application/x-www-form-urlencoded");

            var response = await _client.PostAsync("/UserDictionarySelector", requestBody);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
