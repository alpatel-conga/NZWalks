﻿using NZWalks_Api.Models.Domain;

namespace NZWalks_Api.Repositories
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> GetAllAsync();

        Task<Region> GetAsync(Guid id);

        Task<Region> AddAsync(Region region);

        Task<Region> DeleteRegionAsync(Guid Id);

        Task<Region> UpdateRegionAsync(Guid id,Region region);

    
    }
}
