using DictionaryApplication.Data;
using DictionaryApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

public static class SeedData
{
    public static string CreatedUserId { get; private set; }
    public static int StudiedLangId { get; private set; }
    public static int TranslationLangId { get; private set; }


    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

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

            StudiedLangId = languages[0].Id;
            TranslationLangId = languages[1].Id;

            context.Languages.AddRange(languages);
            context.SaveChanges();
        }

        if (!context.Users.Any())
        {
            var user = new ApplicationUser { Email = "test@gmail.com", UserName = "test@gmail.com", FirstName = "Bohdan", LastName = "Savastieiev" };
            var result = userManager.CreateAsync(user, "YourDesiredPassword").Result;

            if (result.Succeeded)
            {
                CreatedUserId = user.Id;
            }
            else
            {
                // Обработка ошибок при создании пользователя
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new ApplicationException($"Ошибка при создании пользователя: {errors}");
            }
        }
    }
}
