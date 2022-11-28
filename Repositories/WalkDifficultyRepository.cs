using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models;

namespace NZWalks.API.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalksDbContext nZWalksdbContext;
        public WalkDifficultyRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksdbContext = nZWalksDbContext;
        }
        public async Task<WalkDifficulty> AddWalkDiff(WalkDifficulty walkdif)
        {
            walkdif.Id = Guid.NewGuid();
            await nZWalksdbContext.AddAsync(walkdif);
            await nZWalksdbContext.SaveChangesAsync();
            return walkdif;
        }

        public async Task<WalkDifficulty> DeleteWalkDif(Guid id)
        {
            var walkdif = await GetWalkDifById(id);
            if(walkdif == null)
            {
                return null;
            }
            nZWalksdbContext.WalkDifficulty.Remove(walkdif);
            await nZWalksdbContext.SaveChangesAsync();
            return walkdif;
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllWalkDifficulties()
        {
            return await nZWalksdbContext.WalkDifficulty.ToListAsync();
        }

        public async Task<WalkDifficulty> GetWalkDifById(Guid id)
        {
            return await nZWalksdbContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<WalkDifficulty> UpdateWalkDiff(Guid id, WalkDifficulty walkdif)
        {
            var existingWalkdif = await GetWalkDifById(id);

            if(existingWalkdif != null)
            {
                existingWalkdif.Code = walkdif.Code;           
                await nZWalksdbContext.SaveChangesAsync();
                return existingWalkdif;
            }
            return null;
        }
    }
}
