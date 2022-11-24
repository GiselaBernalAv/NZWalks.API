using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext nzWalksdbContext;

        public RegionRepository(NZWalksDbContext nzWalksdbContext)
        {
            this.nzWalksdbContext = nzWalksdbContext;
        }
        public async Task<IEnumerable<Region>> GetAllRegions()
        {
            return await nzWalksdbContext.Regions.ToListAsync();
           
        }
    }
}
