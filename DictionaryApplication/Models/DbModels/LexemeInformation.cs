using Newtonsoft.Json;

namespace DictionaryApplication.Models.DbModels
{
    public class LexemeInformation
    {
        public int Id { get; set; }
        public int TranslatedLexemeId { get; set; }
        public Lexeme? TranslatedLexeme { get; set; }
        public string Translation { get; set; } = null!;
        public List<UsageExample> Examples { get; set; }
        public List<RelatedLexeme> RelatedLexemes { get; set; }
        public LexemeInformation()
        {
            Examples = new List<UsageExample>();
            RelatedLexemes = new List<RelatedLexeme>();
        }
    }
}
