namespace DictionaryApplication.Models.DbModels
{
    public class LexemeExample
    {
        public string Id { get; set; }
        public string LexemeTranslationId { get; set; }
        public LexemeTranslation? LexemeTranslation { get; set; }
        public int NativeLangId { get; set; }
        public Language? NativeLang { get; set; }
        public int TranslatedLangId { get; set; }
        public Language? TranslatedLang { get; set; }
        public string NativeExample { get; set; }
        public string TranslatedExample { get; set; }
    }
}
