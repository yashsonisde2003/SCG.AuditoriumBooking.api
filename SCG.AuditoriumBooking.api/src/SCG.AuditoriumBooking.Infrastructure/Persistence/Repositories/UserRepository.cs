using Microsoft.EntityFrameworkCore;
using SCG.AuditoriumBooking.api.Application.Abstractions.Persistence;
using SCG.AuditoriumBooking.api.Domain.Entities;

namespace SCG.AuditoriumBooking.api.Infrastructure.Persistence.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<User?> FindByEmailAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        return _context.Users.FirstOrDefaultAsync(
            user => user.Email == email,
            cancellationToken);
    }
}