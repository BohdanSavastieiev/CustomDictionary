namespace DictionaryApplication.Models.DbModels
{
    public class UsageExample
    {
        public int Id { get; set; }
        public int LexemeInformationId { get; set; }
        public LexemeInformation? LexemeInformation { get; set; }
        public string NativeExample { get; set; }
        public string TranslatedExample { get; set; }
    }
}
