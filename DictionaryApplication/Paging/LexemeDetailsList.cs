using DictionaryApplication.DTOs;

namespace DictionaryApplication.Paging
{
    public class LexemeDetailsList
    {
        public IEnumerable<LexemeDetailsDto> LexemeDetails { get; set; } = null!;
        public PagingInfo PagingInfo { get; set; } = null!;
    }
}
