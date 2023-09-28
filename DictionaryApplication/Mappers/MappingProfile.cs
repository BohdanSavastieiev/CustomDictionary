using AutoMapper;
using DictionaryApplication.DTOs;
using DictionaryApplication.Models;
using DictionaryApplication.Models.DbModels;

namespace DictionaryApplication.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<LingvoInfoDto, LexemeInputDto>()
             .ForMember(dest => dest.Lexeme, opt => opt.MapFrom(src => src.Lemma))
             .ForMember(dest => dest.Transcription, opt => opt.MapFrom(src => src.Transcription))
             .ForMember(dest => dest.WordForms, opt => opt.MapFrom(src => src.WordForms.Select(wf => new WordFormDto { Word = wf.Text }).ToList()))
             .ForMember(dest => dest.LexemeInformations, opt => opt.MapFrom(src => src.Translations.Select(t => new LexemeInformationDto
             {
                 Translation = t.Text,
                 Examples = t.Examples.Select(e => new UsageExampleDto { NativeExample = e.NativeExample, TranslatedExample = e.TranslatedExample }).ToList(),
                 RelatedLexemes = t.Synonyms.Select(s => new RelatedLexemeDto { Word = s.Text, Type = RelatedLexemeType.Synonym })
                     .Concat(t.Antonyms.Select(a => new RelatedLexemeDto { Word = a.Text, Type = RelatedLexemeType.Antonym }))
                     .Concat(t.DerivedLexemes.Select(d => new RelatedLexemeDto { Word = d.Text, Type = RelatedLexemeType.Derivative }))
                     .ToList()
             }).ToList()));

            CreateMap<Lexeme, LexemeDto>();
            CreateMap<LexemeDto, Lexeme>();
            CreateMap<LexemeInformation, LexemeInformationDto>();
            CreateMap<LexemeInformationDto, LexemeInformation>();
            CreateMap<Models.DbModels.WordForm, WordFormDto>();
            CreateMap<WordFormDto, Models.DbModels.WordForm>();
            CreateMap<UsageExample, UsageExampleDto>();
            CreateMap<UsageExampleDto, UsageExample>();
            CreateMap<RelatedLexeme, RelatedLexemeDto>();
            CreateMap<RelatedLexemeDto, RelatedLexeme>();
        }
    }
}
