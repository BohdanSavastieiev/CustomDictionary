﻿namespace DictionaryApplication.Models.DbModels
{
    public class RelatedLexeme
    {
        public int Id { get; set; }
        public int LexemeInformationId { get; set; }
        public LexemeInformation? LexemeInformation { get; set; }
        public string Word { get; set; }
        public string Type { get; set; }
    }
}
