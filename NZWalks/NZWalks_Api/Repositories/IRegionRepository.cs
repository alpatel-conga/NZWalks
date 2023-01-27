using NZWalks_Api.Models.Domain;

namespace NZWalks_Api.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();
    }
}
