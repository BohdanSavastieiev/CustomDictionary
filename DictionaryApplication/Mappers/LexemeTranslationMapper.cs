using AutoMapper;
using DictionaryApplication.DTOs;
using DictionaryApplication.Models.DbModels;

namespace DictionaryApplication.Mappers
{
    public class LingvoInfoToLexemeInputMapper : ILingvoInfoMapper
    {
        private readonly IMapper _mapper;
        public LingvoInfoToLexemeInputMapper(IMapper mapper)
        {
            _mapper = mapper;
        }
        public LexemeInputDto MapToLexemeInput(LingvoInfoDto lingvoInfoDto)
        {
            LexemeInputDto lexemeInputDto = _mapper.Map<LexemeInputDto>(lingvoInfoDto);

            // Группировка по Translation и выбор наиболее релевантного LexemeInformation
            var groupedLexemeInformations = lexemeInputDto.LexemeInformations
                .GroupBy(li => li.Translation.ToLowerInvariant())
                .Select(group => group.OrderByDescending(li => li.Examples.Count)
                                      .ThenByDescending(li => li.RelatedLexemes.Count)
                                      .First())
                .ToList();

            // Отсортировать по количеству Examples и RelatedLexemes
            var sortedLexemeInformations = groupedLexemeInformations
                .OrderByDescending(li => li.Examples.Count)
                .ThenByDescending(li => li.RelatedLexemes.Count)
                .ToList();

            // Взять первые 4 элемента
            lexemeInputDto.LexemeInformations = sortedLexemeInformations.ToList();

            foreach (var lexemeInformation in lexemeInputDto.LexemeInformations)
            {
                lexemeInformation.Examples = lexemeInformation.Examples.ToList();

                lexemeInformation.RelatedLexemes = lexemeInformation.RelatedLexemes
                    .GroupBy(rl => rl.Type)
                    .SelectMany(group => group)
                    .ToList();
            }

            return lexemeInputDto;
        }

    }
}
