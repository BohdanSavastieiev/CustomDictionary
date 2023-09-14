namespace DictionaryApplication.Models.DbModels
{
    public class RelatedLexeme
    {
        public string Id { get; set; }
        public int LexemeTranslationId { get; set; }
        public LexemeTranslation? LexemeTranslation { get; set; }
        public string Word { get; set; }
        public string Type { get; set; }
    }
}
