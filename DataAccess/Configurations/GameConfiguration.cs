using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Presentation.Constants;

namespace Presentation.Configurations
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.ToTable("Games");

            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Alias)
                .HasMaxLength(EntityConstants.Game.Alias.Max);

            builder
                .HasIndex(x => x.Alias)
                .IsUnique();

            builder
                .Property(x => x.TitleRussian)
                .HasMaxLength(EntityConstants.Game.TitleRussian.Max);

            builder
                .Property(x => x.TitleEnglish)
                .HasMaxLength(EntityConstants.Game.TitleEnglish.Max);

            builder
                .Property(x => x.PhotoUrl)
                .HasMaxLength(EntityConstants.Game.PhotoUrl.Max);

        }
    }
}
