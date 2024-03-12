using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }

        DbSet<Collection> Collections { get; }

        DbSet<Game> Games { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
