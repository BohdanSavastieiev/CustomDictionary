using DictionaryApplication.DTOs;

namespace DictionaryApplication.Mappers
{
    public interface ILingvoInfoMapper
    {
        LexemeInputDto MapToLexemeInput(LingvoInfoDto lingvoInfoDto);
    }
}
