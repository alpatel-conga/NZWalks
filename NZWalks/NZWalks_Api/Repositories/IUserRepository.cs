using NZWalks_Api.Models.Domain;

namespace NZWalks_Api.Repositories
{
    public interface IUserRepository
    {
        Task<User> AuthenticateAsync(string username, string password);
    }
}
