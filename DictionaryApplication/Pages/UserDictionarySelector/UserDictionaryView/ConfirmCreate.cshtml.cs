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
using Newtonsoft.Json;

namespace DictionaryApplication.Pages.UserDictionarySelector.UserDictionaryView
{
    public class ConfirmCreateModel : UserDictionaryViewPageModel
    {
        private readonly ILexemeInputRepository _lexemeInputRepository;
        private readonly ILingvoInfoService _lingvoInfoService;
        public ConfirmCreateModel(
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

            return Page();
        }


        public async Task<IActionResult> OnPostAsync(int userDictionaryId, string lexemeInputSerialized)
        {
            var lexemeInput = JsonConvert.DeserializeObject<LexemeInputDto>(lexemeInputSerialized);

            if (!ModelState.IsValid || lexemeInput == null || !lexemeInput.LexemeInformations.Any())
            {
                return RedirectToPage("Create", new { userDictionaryId = userDictionaryId });
            }

            await _lexemeInputRepository.CreateAsync(userDictionaryId, lexemeInput);

            return RedirectToPage("Index", new { userDictionaryId = userDictionaryId });
        }
    }
}
