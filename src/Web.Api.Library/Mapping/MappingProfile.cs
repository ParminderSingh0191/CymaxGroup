using AutoMapper;
using BestDeal.FakeResponse.Models;
using BestDeal.Web.Api.Library.Models;

namespace BestDeal.Web.Api.Library.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<API1Response, BestDealResponse>()
                .ForMember(dest => dest.Amount, act => act.MapFrom(src => src.Total));

            CreateMap<API2Response, BestDealResponse>()
                .ForMember(dest => dest.Amount, act => act.MapFrom(src => src.Amount));

            CreateMap<API3Response, BestDealResponse>()
                .ForMember(dest => dest.Amount, act => act.MapFrom(src => src.Quote));
        }
    }
}
