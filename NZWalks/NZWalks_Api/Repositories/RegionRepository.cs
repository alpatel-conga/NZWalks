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


        public async Task<Region> GetAsync(Guid id)
        {
            return await nZWalkDbContext.Region.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> AddAsync(Region region)
        {

            region.Id = Guid.NewGuid();
            await nZWalkDbContext.AddAsync(region);
            await nZWalkDbContext.SaveChangesAsync();
            return region;

        }

        public async Task<Region> DeleteRegionAsync(Guid Id)
        {
            var region = await nZWalkDbContext.Region.FirstOrDefaultAsync(x => x.Id == Id);
            if (region == null)
            {
                return null;
            }
            nZWalkDbContext.Region.Remove(region);
            await nZWalkDbContext.SaveChangesAsync();
            return region;

        }
        public async Task<Region> UpdateRegionAsync(Guid id, Region region)
        {
            var extistingregion = await nZWalkDbContext.Region.FirstOrDefaultAsync(x => x.Id == id);

            if (extistingregion == null)
            {
                return null;
            }
            extistingregion.Code = region.Code;
            extistingregion.Name = region.Name;
            extistingregion.Area = region.Area;
            extistingregion.Long = region.Long;
            extistingregion.Lat = region.Lat;
            extistingregion.population = region.population;

            await nZWalkDbContext.SaveChangesAsync();

            return extistingregion;

        }
    }
}
