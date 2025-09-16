using Olympo.Application.Services;
using Olympo.Domain.Entities;
using Olympo.Domain.Entities.Users;

namespace Olympo.Application.Features.Authentication;

public class RegisterHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticationService _authenticationService;

    public RegisterHandler(IUserRepository userRepository, IAuthenticationService authenticationService)
    {
        _userRepository = userRepository;
        _authenticationService = authenticationService;
    }

    public async Task<RegisterResponse> HandleAsync(RegisterRequest request)
    {
        // Check if user already exists
        if (await _userRepository.ExistsByEmailAsync(request.Email))
        {
            throw new InvalidOperationException("A user with this email already exists.");
        }

        // Parse fitness level if provided
        FitnessLevel? fitnessLevel = null;
        if (!string.IsNullOrEmpty(request.FitnessLevel))
        {
            if (Enum.TryParse<FitnessLevel>(request.FitnessLevel, true, out var parsed))
            {
                fitnessLevel = parsed;
            }
        }

        // Create new user
        var user = new User
        {
            Email = request.Email.ToLower(),
            PasswordHash = _authenticationService.HashPassword(request.Password),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Weight = request.Weight,
            Height = request.Height,
            FitnessLevel = fitnessLevel,
            Role = UserRole.User
        };

        await _userRepository.AddAsync(user);

        return new RegisterResponse
        {
            UserId = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Message = "User registered successfully."
        };
    }
}
