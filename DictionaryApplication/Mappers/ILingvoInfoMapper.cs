using DictionaryApplication.DTOs;
using DictionaryApplication.Models;

namespace DictionaryApplication.Mappers
{
    public interface ILingvoInfoMapper
    {
        LexemeInput MapToLexemeInput(LingvoInfoDto lingvoInfoDto);
    }
}
