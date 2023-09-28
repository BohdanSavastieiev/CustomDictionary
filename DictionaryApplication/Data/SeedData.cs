using DictionaryApplication.Data;
using DictionaryApplication.Models;
using DictionaryApplication.Repositories;
using DictionaryApplication.Services;
using Newtonsoft.Json;

namespace DictionaryApplication.Data
{
    public static class SeedData
    {
        public async static Task Initialize(
            ApplicationDbContext context,
            ILingvoInfoService lingvoInfoService,
            ILexemeInputRepository lexemeInputRepository)
        {
            if (!context.Languages.Any())
            {
                var languages = new List<Language>
                {
                    new Language { LangCode = "ENG", Name = "English" },
                    new Language { LangCode = "RUS", Name = "Russian" },
                    new Language { LangCode = "UAH", Name = "Ukrainian" },
                    new Language { LangCode = "SPA", Name = "Spanish" },
                    new Language { LangCode = "GER", Name = "German" },

                };

                context.Languages.AddRange(languages);
                context.SaveChanges();
            }
        }
    }

}
