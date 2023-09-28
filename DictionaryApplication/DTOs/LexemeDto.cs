using DictionaryApplication.Models.DbModels;
using DictionaryApplication.Models;

namespace DictionaryApplication.DTOs
{
    public class LexemeDto
    {
        public int Id { get; set; }
        public int DictionaryId { get; set; }
        public string Word { get; set; } = null!;
        public int TotalTestAttempts { get; set; }
        public int CorrectTestAttempts { get; set; }
        public string? Transcription { get; set; }
        public List<LexemeInformationDto> LexemeInformations { get; set; }
        public List<WordFormDto> WordForms { get; set; }
        public LexemeDto()
        {
            LexemeInformations = new List<LexemeInformationDto>();
            WordForms = new List<WordFormDto>();
        }
    }
}
