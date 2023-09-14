using DictionaryApplication.Models.DbModels;
using Microsoft.AspNetCore.Mvc;

namespace DictionaryApplication.Models
{
    public class LexemeInput
    {
        public string Lexeme { get; set; } = null!;
        public List<string> Translations { get; set; } = null!;
        public string? Description { get; set; }
        public string? Transcription { get; set; }
        public List<LexemeTranslation> LexemeTranslations { get; set; }
        public LexemeInput()
        {
            LexemeTranslations = new List<LexemeTranslation>();
        }
    }
}
