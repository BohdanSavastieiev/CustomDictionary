using Newtonsoft.Json;

namespace DictionaryApplication.Models.DbModels
{
    public class LexemeInformation
    {
        public int Id { get; set; }
        public int TranslatedLexemeId { get; set; }
        [JsonIgnore]
        public Lexeme? TranslatedLexeme { get; set; }
        public string Translation { get; set; }
        public ICollection<UsageExample>? Examples { get; set; }
        public ICollection<RelatedLexeme>? RelatedLexemes { get; set; }
    }
}
