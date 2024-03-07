using DomainLayer.Entities;
using System.Data.Entity;

namespace DataAccess
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }

        DbSet<Collection> Collections { get; }

        DbSet<Game> Games { get; }
    }
}
