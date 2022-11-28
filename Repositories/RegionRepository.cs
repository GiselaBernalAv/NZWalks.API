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

        public async Task<Region> GetRegionById(Guid id)
        {
            return await nzWalksdbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> AddRegion(Region region)
        {
            region.Id = Guid.NewGuid();
            await nzWalksdbContext.AddAsync(region);
            await nzWalksdbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteRegion(Guid id)
        {
            var region = await GetRegionById(id);
            if(region == null)
            {
                return null;
            }
            nzWalksdbContext.Regions.Remove(region);
            await nzWalksdbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> UpdateRegion(Guid id, Region region)
        {
            var existingRegion =  await GetRegionById(id);

            if(region == null)
                return null;

            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.Area = region.Area;
            existingRegion.Lat = region.Lat;
            existingRegion.LongReg = region.LongReg;
            existingRegion.Population = region.Population;

            await nzWalksdbContext.SaveChangesAsync();

            return existingRegion;


        }
    }
}
