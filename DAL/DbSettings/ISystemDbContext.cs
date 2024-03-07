using System.Collections.Generic;

namespace DAL.DbSettings
{
    public interface ISystemDbContext
    {
        DbSet<User> Users { get; }

        DbSet<Collection> Collections { get; }

        DbSet<Game> Games { get; }

    }
}
