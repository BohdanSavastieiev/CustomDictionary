using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DictionaryApplication.Data;
using DictionaryApplication.Repositories;
using DictionaryApplication.DTOs;
using DictionaryApplication.Extensions;
using Newtonsoft.Json;

namespace DictionaryApplication.Pages.UserDictionarySelector.UserDictionaryView
{
    public class EditModel : UserDictionaryViewPageModel
    {
        private readonly ILexemeInputRepository _lexemeInputRepository;

        public EditModel(
            ILexemeInputRepository lexemeInputRepository,
            IUserDictionaryRepository userDictionaryRepository) : base(userDictionaryRepository)
        {
            _lexemeInputRepository = lexemeInputRepository;
        }

        public LexemeInputDto LexemeInput { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int userDictionaryId, int lexemeId)
        {
            await LoadUserDictionaryAsync(userDictionaryId);
            var lexemeInput = await _lexemeInputRepository.GetByIdAsync(lexemeId);
            if (lexemeInput == null)
            {
                return RedirectToPage("./Index", new { userDictionaryId = userDictionaryId });
            }
            else
            {
                LexemeInput = lexemeInput;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int userDictionaryId, int lexemeId, string lexemeInputSerialized)
        {
            var lexemeInput = JsonConvert.DeserializeObject<LexemeInputDto>(lexemeInputSerialized);

            if (!ModelState.IsValid || lexemeInput == null || !lexemeInput.LexemeInformations.Any())
            {
                return RedirectToPage("./Index", new { userDictionaryId = userDictionaryId });
            }

            await _lexemeInputRepository.UpdateAsync(lexemeId, lexemeInput);

            return RedirectToPage("./Index", new { userDictionaryId = userDictionaryId });
        }
    }
}
