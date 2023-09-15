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
using DictionaryApplication.DTOs;

namespace DictionaryApplication.Pages.UserDictionarySelector.UserDictionaryView
{
    public class CreateWithDataModel : UserDictionaryViewPageModel
    {
        private readonly ILexemeInputRepository _lexemeInputRepository;
        private readonly ILingvoInfoService _lingvoInfoService;
        public CreateWithDataModel(
            ILexemeInputRepository lexemeInputRepository,
            ILingvoInfoService lingvoInfoService,
            IUserDictionaryRepository userDictionaryRepository) : base(userDictionaryRepository)
        {
            _lexemeInputRepository = lexemeInputRepository;
            _lingvoInfoService = lingvoInfoService;
        }

        public LexemeInputDto LexemeInput { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int userDictionaryId, string lexeme)
        {
            await LoadUserDictionaryAsync(userDictionaryId);

            var srcLang = UserDictionary.StudiedLanguage.LangCode.Substring(0, 2).ToLowerInvariant();
            var dstLang = UserDictionary.TranslationLanguage.LangCode.Substring(0, 2).ToLowerInvariant();

            var lexemeInput = await _lingvoInfoService.GetLingvoInfoAsync(lexeme, srcLang, dstLang, false);
            LexemeInput = lexemeInput;
            HttpContext.Session.SetObject<LexemeInputDto>("lexemeInput", lexemeInput);

            return Page();
        }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int userDictionaryId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var lexemeInput = HttpContext.Session.GetObject<LexemeInputDto>("lexemeInput");
            if (lexemeInput != null)
            {
                await _lexemeInputRepository.CreateAsync(userDictionaryId, lexemeInput);
                HttpContext.Session.Remove("lexemeInput");
            }


            return RedirectToPage("Index", new { userDictionaryId = userDictionaryId });
        }
    }
}
