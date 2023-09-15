using DictionaryApplication.Models.DbModels;
using Microsoft.AspNetCore.Mvc;

namespace DictionaryApplication.DTOs
{
    public class LexemeInputDto
    {
        public string Lexeme { get; set; } = null!;
        public string? Description { get; set; }
        public string? Transcription { get; set; }
        public List<LexemeInformationDto> LexemeInformations { get; set; }
        public List<WordFormDto> WordForms { get; set; }
        public LexemeInputDto()
        {
            LexemeInformations = new List<LexemeInformationDto>();
        }
    }
}
