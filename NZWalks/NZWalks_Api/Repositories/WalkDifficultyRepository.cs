﻿using Microsoft.EntityFrameworkCore;
using NZWalks_Api.Data;
using NZWalks_Api.Models.Domain;

namespace NZWalks_Api.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalkDbContext nZWalksDbContext;

        public WalkDifficultyRepository(NZWalkDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<WalkDifficulty> AddAsync(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = Guid.NewGuid();
            await nZWalksDbContext.WalkDifficulty.AddAsync(walkDifficulty);
            await nZWalksDbContext.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<WalkDifficulty> DeleteAsync(Guid id)
        {
            var existingWalkDifficulty = await nZWalksDbContext.WalkDifficulty.FindAsync(id);
            if (existingWalkDifficulty != null)
            {
                nZWalksDbContext.WalkDifficulty.Remove(existingWalkDifficulty);
                await nZWalksDbContext.SaveChangesAsync();
                return existingWalkDifficulty;
            }
            return null;
        }

        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            return await nZWalksDbContext.WalkDifficulty.ToListAsync();
        }

        public async Task<WalkDifficulty> GetAsync(Guid id)
        {
            return await nZWalksDbContext.WalkDifficulty.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<WalkDifficulty> UpdateAsync(Guid id, WalkDifficulty walkDifficulty)
        {
            var existingWalkDifficulty = await nZWalksDbContext.WalkDifficulty.FindAsync(id);

            if (existingWalkDifficulty == null)
            {
                return null;
            }

            existingWalkDifficulty.Code = walkDifficulty.Code;
            await nZWalksDbContext.SaveChangesAsync();
            return existingWalkDifficulty;
        }
    }
}
