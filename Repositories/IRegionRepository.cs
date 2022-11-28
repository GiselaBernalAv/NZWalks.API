using NZWalks.API.Models;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllRegions();

        Task<Region> GetRegionById(Guid id);

        Task<Region> AddRegion(Region region);

        Task<Region> DeleteRegion(Guid id);

        Task<Region> UpdateRegion(Guid id, Region region);
    }
}
