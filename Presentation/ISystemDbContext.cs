using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;


namespace Presentation
{
    public interface ISystemDbContext
    {
        DbSet<User> Users { get; }

        DbSet<Collection> Collections { get; }

        DbSet<Game> Games { get; }

    }
}
