using NZWalks.API.Models;

namespace NZWalks.API.Repositories
{
    public interface IWalkDifficultyRepository
    {
        Task<IEnumerable<WalkDifficulty>> GetAllWalkDifficulties();
        Task<WalkDifficulty> GetWalkDifById(Guid id);

        Task<WalkDifficulty> AddWalkDiff(WalkDifficulty walkdif);

        Task<WalkDifficulty> DeleteWalkDif(Guid id);

        Task<WalkDifficulty> UpdateWalkDiff(Guid id, WalkDifficulty walkdif);
    }
}
