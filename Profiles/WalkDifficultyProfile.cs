using AutoMapper;

namespace NZWalks.API.Profiles
{
    public class WalkDifficultyProfile : Profile
    {
        public WalkDifficultyProfile()
        {
            CreateMap<Models.WalkDifficulty, Models.DTO.WalkDifficulty>().ReverseMap();

        }
    }
}
