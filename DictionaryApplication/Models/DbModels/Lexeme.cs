using DictionaryApplication.DTOs;
using DictionaryApplication.Models.DbModels;

namespace DictionaryApplication.Models
{
    public class Lexeme
    {
        public int Id { get; set; }
        public int DictionaryId { get; set; }
        public UserDictionary? Dictionary { get; set; } = null!;
        public string Word { get; set; } = null!;
        public int TotalTestAttempts { get; set; }
        public int CorrectTestAttempts { get; set; }
        public string? Transcription { get; set; }
        public List<LexemeInformation> LexemeInformations { get; set; }
        public List<DbModels.WordForm> WordForms { get; set; }
        public Lexeme()
        {
            LexemeInformations = new List<LexemeInformation>();
            WordForms = new List<DbModels.WordForm>();
        }
    }
}
