using AutoMapper;

namespace NZWalks.API.Profiles
{
    public class WalksProfile : Profile
    {
        public WalksProfile()
        {
            CreateMap<Models.Walk, Models.DTO.Walk>().ReverseMap();

            CreateMap<Models.WalkDifficulty, Models.DTO.WalkDifficulty>().ReverseMap();
        }
    }
}
