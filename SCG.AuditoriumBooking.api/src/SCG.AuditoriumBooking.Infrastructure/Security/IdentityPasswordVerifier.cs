using Microsoft.AspNetCore.Identity;
using SCG.AuditoriumBooking.api.Application.Abstractions.Security;
using SCG.AuditoriumBooking.api.Domain.Entities;

namespace SCG.AuditoriumBooking.api.Infrastructure.Security;

public sealed class IdentityPasswordVerifier : IPasswordVerifier
{
    private readonly IPasswordHasher<User> _passwordHasher;

    public IdentityPasswordVerifier(IPasswordHasher<User> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    public bool Verify(User user, string providedPassword)
    {
        var result = _passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            providedPassword);

        return result != PasswordVerificationResult.Failed;
    }
}