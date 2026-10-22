using SCG.AuditoriumBooking.api.Domain.Entities;

namespace SCG.AuditoriumBooking.api.Application.Abstractions.Security;

public interface ITokenService
{
    GeneratedToken CreateToken(User user);
}

public sealed record GeneratedToken(string Token, DateTime ExpiresAt);