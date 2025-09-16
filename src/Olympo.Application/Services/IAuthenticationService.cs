using Olympo.Domain.Entities;
using Olympo.Domain.Entities.Users;

namespace Olympo.Application.Services;

public interface IAuthenticationService
{
    string GenerateJwtToken(User user);
    string HashPassword(string password);
    bool VerifyPassword(string password, string hash);
}