using NZWalks_Api.Models.Domain;

namespace NZWalks_Api.Repositories
{
    public interface IUserRepository
    {
        Task<User> AuthenticateAsync(string username, string password);

        Task<IEnumerable<User>> GetAllUsersAsync();

        Task<User> GetUserAsync(Guid id);

        Task<User> AddUserAsync(User user);

        Task<User> DeleteUserAsync(Guid Id);

    }
}
