using SCG.AuditoriumBooking.api.Application.Abstractions.Persistence;
using SCG.AuditoriumBooking.api.Application.Abstractions.Security;

namespace SCG.AuditoriumBooking.api.Application.Features.Authentication;

public sealed class LoginService
{
    private const string InvalidCredentialsMessage =
        "Invalid email or password.";

    private readonly IUserRepository _users;
    private readonly IPasswordVerifier _passwordVerifier;
    private readonly ITokenService _tokenService;

    public LoginService(
        IUserRepository users,
        IPasswordVerifier passwordVerifier,
        ITokenService tokenService)
    {
        _users = users;
        _passwordVerifier = passwordVerifier;
        _tokenService = tokenService;
    }

    public async Task<LoginResponse?> LoginAsync(
        LoginRequest request,
        CancellationToken cancellationToken = default)
    {
        var user = await _users.FindByEmailAsync(
            request.Email,
            cancellationToken);

        if (user is null || !user.IsActive)
        {
            return null;
        }

        if (!_passwordVerifier.Verify(user, request.Password))
        {
            return null;
        }

        var generatedToken = _tokenService.CreateToken(user);

        return new LoginResponse
        {
            Token = generatedToken.Token,
            ExpiresAt = generatedToken.ExpiresAt,
            UserId = user.UserId,
            Name = user.Name,
            Email = user.Email,
            RoleName = user.RoleName,

        };
    }
}