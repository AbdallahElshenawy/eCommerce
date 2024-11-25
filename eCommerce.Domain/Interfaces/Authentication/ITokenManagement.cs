using System.Security.Claims;

namespace eCommerce.Domain.Interfaces.Authentication
{
    public interface ITokenManagement
    {
        string GetRefreshToken();
        List<Claim> GetUserClaims(string token);
        Task<bool> ValidateRefreshToken(string refreshtoken);
        Task<string>GetUserIdByRefreshToken(string refreshtoken);
        Task<int> AddRefreshToken(string userId,string refreshtoken);
        Task<int> UpdateRefreshToken(string userId,string refreshtoken);
        string GenerateToken(List<Claim> claims);

    }
}
