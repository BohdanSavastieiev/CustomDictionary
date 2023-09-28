using DictionaryApplication.Models.DbModels;
using DictionaryApplication.Models;

namespace DictionaryApplication.DTOs
{
    public class LexemeInformationDto
    {
        public string Translation { get; set; } = null!;
        public List<UsageExampleDto> Examples { get; set; }
        public List<RelatedLexemeDto> RelatedLexemes { get; set; }
        public LexemeInformationDto()
        {
            Examples = new List<UsageExampleDto>();
            RelatedLexemes = new List<RelatedLexemeDto>();
        }
    }
}
