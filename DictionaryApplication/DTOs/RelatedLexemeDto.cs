using DictionaryApplication.Models.DbModels;

namespace DictionaryApplication.DTOs
{
    public class RelatedLexemeDto
    {
        public string Word { get; set; }
        public RelatedLexemeType Type { get; set; }
    }

    public enum RelatedLexemeType
    {
        Synonym,
        Antonym,
        DerivedLexeme
    }
}
