using DictionaryApplication.Pages.KnowledgeTest;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;

namespace DictionaryApplication.Models
{
    [Serializable]
    public class KnowledgeTestParameters
    {
        [Required(ErrorMessage = "At least one dictionary must be selected.")]
        public List<int> SelectedDictionaryIds { get; set; } = null!;
        [Required(ErrorMessage = "Number of words is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Number of words must be greater than zero.")]
        public int NumberOfWords { get; set; }
        [Required(ErrorMessage = "Please select translation direction.")]
        public bool IsUserTranslatesStudiedLanguage { get; set; }
        [Required(ErrorMessage = "Test type is required.")]
        public TestType TestType { get; set; }
    }

    public enum TestType
    {
        AllWords,
        LastWords,
        WordsWithWorstResults
    }
}
