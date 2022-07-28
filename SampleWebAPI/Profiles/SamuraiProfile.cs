using AutoMapper;
using SampleWebAPI.Domain;
using SampleWebAPI.DTO;

namespace SampleWebAPI.Profiles
{
    public class SamuraiProfile : Profile
    {
        public SamuraiProfile()
        {
            CreateMap<SamuraiWithQuotesDTO, Samurai>();
            CreateMap<Samurai, SamuraiWithQuotesDTO>();

            //CreateMap<SamuraiWithSwordDTO, Samurai>();
            //CreateMap<Samurai, SamuraiWithSwordDTO>();


            CreateMap<SwordReadDTO, Sword>();
            CreateMap<Sword, SwordReadDTO>();
            CreateMap<SwordCreateDTO, Sword>();
            CreateMap<Sword, SwordCreateDTO>();
            CreateMap<SwordWithElementDTO, Sword>();
            CreateMap<Sword, SwordWithElementDTO>();
            CreateMap<Sword, AddSwordToExistingElementDTO>();
            CreateMap<AddSwordToExistingElementDTO, Sword>();

            CreateMap<Samurai, SamuraiReadDTO>();
            CreateMap<SamuraiReadDTO, Samurai>();
            CreateMap<SamuraiCreateDTO, Samurai>();
            CreateMap<SamuraiWithFullAttributeDTO, Samurai>();
            CreateMap<Samurai, SamuraiWithFullAttributeDTO>();

            CreateMap<ElementReadDTO, Element>();
            CreateMap<Element, ElementReadDTO>();
            CreateMap<ElementCreateDTO, Element>();
            CreateMap<Element, ElementCreateDTO>();

            CreateMap<AddSamuraiWithSwordDTO, Samurai>();
            CreateMap<Samurai, AddSamuraiWithSwordDTO > ();
            
            CreateMap<QuoteDTO, Quote>();
            CreateMap<Quote, QuoteDTO>();

            CreateMap<SwordTypeReadDTO, SwordType>();
            CreateMap<SwordType, SwordTypeReadDTO>();
        }
    }
}
