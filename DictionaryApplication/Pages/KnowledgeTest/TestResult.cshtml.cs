using DictionaryApplication.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DictionaryApplication.Services;
using DictionaryApplication.Extensions;
using Microsoft.AspNetCore.Authorization;
using DictionaryApplication.DTOs;

namespace DictionaryApplication.Pages.KnowledgeTest
{
    [Authorize]
    public class TestResultModel : PageModel
    {
        private KnowledgeTestService _knowledgeTestService;
        public TestResultModel(KnowledgeTestService knowledgeTestService) 
        {
            _knowledgeTestService = knowledgeTestService;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            await HttpContext.Session.LoadAsync();

            var lexemeTestAttempts = HttpContext.Session.GetList<LexemeTestAttemptDto>("lexemeTestAttempts");

            await _knowledgeTestService.SetResults(lexemeTestAttempts);

            int totalAnswers = lexemeTestAttempts.Count;
            int totalCorrectAnswers = lexemeTestAttempts.Count(ta => ta.IsCorrectAnswer);
            double correctAnswersPercentage = Math.Round((double)totalCorrectAnswers / totalAnswers * 100, 2);

            ViewData["TotalAnswers"] = totalAnswers;
            ViewData["TotalCorrectAnswers"] = totalCorrectAnswers;
            ViewData["CorrectAnswersPercentage"] = correctAnswersPercentage;

            HttpContext.Session.Remove("knowledgeTestParameters");
            HttpContext.Session.Remove("lexemeTestAttempts");

            await HttpContext.Session.CommitAsync();

            return Page();
        }
    }
}
