using DictionaryApplication.Models.DbModels;
using DictionaryApplication.Models;

namespace DictionaryApplication.DTOs
{
    public class LexemeInformationDto
    {
        public string Translation { get; set; }
        public List<UsageExampleDto>? Examples { get; set; }
        public List<RelatedLexemeDto>? RelatedLexemes { get; set; }
    }
}
