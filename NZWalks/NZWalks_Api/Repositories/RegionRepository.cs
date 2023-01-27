using Microsoft.EntityFrameworkCore;
using NZWalks_Api.Data;
using NZWalks_Api.Models.Domain;

namespace NZWalks_Api.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalkDbContext nZWalkDbContext;

        public RegionRepository(NZWalkDbContext nZWalkDbContext) 
        {
            this.nZWalkDbContext = nZWalkDbContext;
        }
        public async Task<IEnumerable<Region>> GetAllAsync()
        {
           return await nZWalkDbContext.Region.ToListAsync();
        }
    }
}
