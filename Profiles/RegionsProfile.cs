using AutoMapper;

namespace NZWalks.API.Profiles
{
    public class RegionsProfile: Profile
    {
        public RegionsProfile()
        {
            CreateMap<Models.Region, Models.DTO.Region>().ReverseMap();

            //if the model field is different than the dto's field the map is like this, if region.id had of name regionid for example
            //CreateMap<Models.Region, Models.DTO.Region>().ForMember(
            //                         dest=>dest.Id, options=>options.MapFrom(src=>src.Id));
        }
        
    }
}
