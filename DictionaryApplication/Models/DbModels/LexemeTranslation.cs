namespace DictionaryApplication.Models.DbModels
{
    public class LexemeTranslation
    {
        public string Id { get; set; }
        public int TranslatedLexemeId { get; set; }
        public Lexeme? TranslatedLexeme { get; set; }
        public string Word { get; set; }
        public ICollection<LexemeExample> Examples { get; set; }
        public ICollection<RelatedLexeme> Synonyms { get; set; }
        public ICollection<RelatedLexeme> Antonyms { get; set; }
        public ICollection<RelatedLexeme> DerivedLexemes { get; set; }
    }
}
