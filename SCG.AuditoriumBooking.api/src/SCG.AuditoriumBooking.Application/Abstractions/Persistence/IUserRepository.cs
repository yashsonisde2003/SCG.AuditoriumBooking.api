using SCG.AuditoriumBooking.api.Domain.Entities;

namespace SCG.AuditoriumBooking.api.Application.Abstractions.Persistence;

public interface IUserRepository
{
    Task<User?> FindByEmailAsync(
        string email,
        CancellationToken cancellationToken = default);
}