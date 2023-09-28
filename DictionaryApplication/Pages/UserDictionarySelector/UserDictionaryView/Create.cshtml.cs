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
    public class CreateModel : UserDictionaryViewPageModel
    {
        private readonly ILexemeInputRepository _lexemeInputRepository;
        public CreateModel(
            ILexemeInputRepository lexemeInputRepository,
            IUserDictionaryRepository userDictionaryRepository) : base(userDictionaryRepository)
        {
            _lexemeInputRepository = lexemeInputRepository;
        }

        [BindProperty]
        public string Lexeme { get; set; } = null!;
        public bool IsLexemeValid => !string.IsNullOrWhiteSpace(Lexeme);

        public async Task<IActionResult> OnGetAsync(int userDictionaryId)
        {
            await LoadUserDictionaryAsync(userDictionaryId);

            return Page();
        }        


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int userDictionaryId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return RedirectToPage("ConfirmCreate", new { userDictionaryId = userDictionaryId, lexeme = Lexeme });
        }
    }
}
