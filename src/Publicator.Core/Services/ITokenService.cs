namespace Publicator.Core.Services
{
    public interface ITokenService
    {
        string GenerateToken(Infrastructure.Models.User user);
    }
}
