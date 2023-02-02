using NZWalks_Api.Models.Domain;

namespace NZWalks_Api.Repositories
{
    public interface ITokenHandler
    {
        Task<string> CreateTokenAsync(User user);
    }
}
