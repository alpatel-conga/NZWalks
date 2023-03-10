using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NZWalks_Api.Data;
using NZWalks_Api.Models.Domain;

namespace NZWalks_Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly NZWalkDbContext _nZWalkDbContext;
        private readonly IMapper mapper;

        public UserRepository(NZWalkDbContext nZWalkDbContext,IMapper mapper) 
        {
            _nZWalkDbContext = nZWalkDbContext;
            this.mapper = mapper;
        }
        public async Task<User> AddUserAsync(User user)
        {
            user.Id = Guid.NewGuid();
            await _nZWalkDbContext.Users.AddAsync(user);
            await _nZWalkDbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {

            var user = await _nZWalkDbContext.Users.FirstOrDefaultAsync(x => x.Password
                                                                   == password);
            //var user =await _nZWalkDbContext.Users.FirstOrDefaultAsync(x => x.Username.ToLower()==username.ToLower());
            
         

            return user;
        }

        public async Task<User> DeleteUserAsync(Guid Id)
        {
            var user=await _nZWalkDbContext.Users.FirstOrDefaultAsync(x => x.Id == Id);
            if(user == null)
            {
                return null;
            }
            _nZWalkDbContext.Users.Remove(user);
            await _nZWalkDbContext.SaveChangesAsync();
            return user;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _nZWalkDbContext.Users.ToListAsync();
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            return await _nZWalkDbContext.Users.FirstOrDefaultAsync(x=>x.Id == id);
        }
    }
}
