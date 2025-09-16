using Olympo.Application.Services;
using Olympo.Domain.Entities;
using Olympo.Domain.Entities.Users;

namespace Olympo.Application.Features.Authentication;

public class LoginHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticationService _authenticationService;

    public LoginHandler(IUserRepository userRepository, IAuthenticationService authenticationService)
    {
        _userRepository = userRepository;
        _authenticationService = authenticationService;
    }

    public async Task<LoginResponse> HandleAsync(LoginRequest request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        
        if (user == null || !_authenticationService.VerifyPassword(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        var token = _authenticationService.GenerateJwtToken(user);
        var expiresAt = DateTime.UtcNow.AddHours(24);

        return new LoginResponse
        {
            Token = token,
            UserId = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Role = user.Role.ToString(),
            ExpiresAt = expiresAt
        };
    }
}
