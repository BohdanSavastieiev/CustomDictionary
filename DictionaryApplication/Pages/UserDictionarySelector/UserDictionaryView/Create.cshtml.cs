using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DictionaryApplication.Data;
using DictionaryApplication.Models;
using Microsoft.EntityFrameworkCore;
using DictionaryApplication.Repositories;
using Microsoft.AspNetCore.Authorization;
using DictionaryApplication.Services;
using DictionaryApplication.Extensions;

namespace DictionaryApplication.Pages.UserDictionarySelector.UserDictionaryView
{
    public class CreateModel : UserDictionaryViewPageModel
    {
        private readonly ILexemeInputRepository _lexemeInputRepository;
        private readonly ILingvoInfoService _lingvoInfoService;
        public CreateModel(
            ILexemeInputRepository lexemeInputRepository,
            ILingvoInfoService lingvoInfoService,
            IUserDictionaryRepository userDictionaryRepository) : base(userDictionaryRepository)
        {
            _lexemeInputRepository = lexemeInputRepository;
            _lingvoInfoService = lingvoInfoService;
        }

        [BindProperty]
        public LexemeInput LexemeInput { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int userDictionaryId)
        {
            await LoadUserDictionaryAsync(userDictionaryId);

            var sessionLexemeInput = HttpContext.Session.GetObject<LexemeInput>("lexemeInput");
            if (sessionLexemeInput != null)
            {
                LexemeInput = sessionLexemeInput;
            }

            return Page();
        }        


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int userDictionaryId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _lexemeInputRepository.CreateAsync(userDictionaryId, LexemeInput);

            HttpContext.Session.Remove("lexemeInput");

            return RedirectToPage("Index", new { userDictionaryId = userDictionaryId});
        }

        public async Task<IActionResult> OnPostAsync(int userDictionaryId, string lexeme)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var srcLang = UserDictionary.StudiedLanguage.LangCode.Take(2).ToString().ToLowerInvariant();
            var dstLang = UserDictionary.TranslationLanguage.LangCode.Take(2).ToString().ToLowerInvariant();

            var lexemeInput = await _lingvoInfoService.GetLingvoInfoAsync(lexeme, srcLang, dstLang, false);
            HttpContext.Session.SetObject<LexemeInput>("lexemeInput", lexemeInput);


            return RedirectToPage("Create", new { userDictionaryId = userDictionaryId });
        }
    }
}
