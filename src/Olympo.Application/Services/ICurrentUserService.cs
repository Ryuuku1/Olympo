using Olympo.Domain.Entities.Users;

namespace Olympo.Application.Services;

public interface ICurrentUserService
{
    Guid UserId { get; }
    string Email { get; }
    UserRole Role { get; }
    bool IsAuthenticated { get; }
}