using SCG.AuditoriumBooking.api.Domain.Entities;

namespace SCG.AuditoriumBooking.api.Application.Abstractions.Security;

public interface IPasswordVerifier
{
    bool Verify(User user, string providedPassword);
}