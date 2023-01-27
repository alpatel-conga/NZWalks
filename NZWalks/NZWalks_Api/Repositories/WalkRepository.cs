using Microsoft.EntityFrameworkCore;
using NZWalks_Api.Data;
using NZWalks_Api.Models.Domain;

namespace NZWalks_Api.Repositories
{
    public class WalkRepository:IWalkRepository
    {
        private readonly NZWalkDbContext nZWalkDbContext;

        public WalkRepository(NZWalkDbContext nZWalksDbContext)
        {
            this.nZWalkDbContext = nZWalksDbContext;
        }

        public async Task<Walk> AddAsync(Walk walk)
        {
            // Assign New ID
            walk.Id = Guid.NewGuid();
            await nZWalkDbContext.Walk.AddAsync(walk);
            await nZWalkDbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk> DeleteAsync(Guid id)
        {
            var existingWalk = await nZWalkDbContext.Walk.FindAsync(id);

            if (existingWalk == null)
            {
                return null;
            }

            nZWalkDbContext.Walk.Remove(existingWalk);
            await nZWalkDbContext.SaveChangesAsync();
            return existingWalk;
        }

        public async Task<IEnumerable<Walk>> GetAllAsync()
        {
            return await
                nZWalkDbContext.Walk
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .ToListAsync();
        }

        public Task<Walk> GetAsync(Guid id)
        {
            return nZWalkDbContext.Walk
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await nZWalkDbContext.Walk.FindAsync(id);

            if (existingWalk != null)
            {
                existingWalk.Length = walk.Length;
                existingWalk.Name = walk.Name;
                existingWalk.WalkDifficultyId = walk.WalkDifficultyId;
                existingWalk.RegionId = walk.RegionId;
                await nZWalkDbContext.SaveChangesAsync();
                return existingWalk;
            }

            return null;
        }
    }
}
