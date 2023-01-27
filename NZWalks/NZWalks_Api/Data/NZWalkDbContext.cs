using Microsoft.EntityFrameworkCore;
using NZWalks_Api.Models.Domain;

namespace NZWalks_Api.Data
{
    public class NZWalkDbContext:DbContext
    {
        public NZWalkDbContext(DbContextOptions<NZWalkDbContext> options):base(options) 
        {
        
        
        }
        public DbSet<Region> Region { get; set; }

        public DbSet<Walk> Walk { get; set; }

        public DbSet<WalkDifficulty> WalkDifficulty { get; set; }
      
    }
}
