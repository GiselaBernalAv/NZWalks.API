using NZWalks.API.Models;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetAllWalks();
        Task<Walk> GetWalkById(Guid id);

        Task<Walk> AddWalk(Walk walk);

        Task<Walk> DeleteWalk(Guid id);

        Task<Walk> UpdateWalk(Guid id, Walk walk);
    }
}
