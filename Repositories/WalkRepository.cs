using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models;

namespace NZWalks.API.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext nzWalksdbContext;

        public WalkRepository(NZWalksDbContext nzWalksdbContext)
        {
            this.nzWalksdbContext = nzWalksdbContext;
        }

        public async Task<Walk> AddWalk(Walk walk)
        {
            walk.Id = Guid.NewGuid();
            await nzWalksdbContext.AddAsync(walk);
            await nzWalksdbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> DeleteWalk(Guid id)
        {
            var walk = await GetWalkById(id);
            if(walk == null)
            {
                return null;
            }
            nzWalksdbContext.Walks.Remove(walk);
            await nzWalksdbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<IEnumerable<Walk>> GetAllWalks()
        {
            return await nzWalksdbContext.Walks
                .Include(x=>x.WalkDifficulty)
                .Include(x=>x.Region)
                .ToListAsync();
        }

        public async Task<Walk> GetWalkById(Guid id)
        {
            return await nzWalksdbContext.Walks
                  .Include(x => x.WalkDifficulty)
                  .Include(x => x.Region)
                  .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk> UpdateWalk(Guid id, Walk walk)
        {
            var existingWalk = await GetWalkById(id);

            if(existingWalk != null)
            {
                existingWalk.LongWalk = walk.LongWalk;  
                existingWalk.Name = walk.Name;
                existingWalk.RegionId = walk.RegionId;
                existingWalk.WalkDifficultyId = walk.WalkDifficultyId;
                await nzWalksdbContext.SaveChangesAsync();
                return existingWalk;
            }
            return null;
           
        }
    }
}
