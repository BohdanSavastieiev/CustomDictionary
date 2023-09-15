using Newtonsoft.Json;

namespace DictionaryApplication.Models.DbModels
{
    public class WordForm
    {
        public int Id { get; set; }
        public int LexemeId { get; set; }
        [JsonIgnore]
        public Lexeme Lexeme { get; set;}
        public string Word { get; set; }
    }
}
