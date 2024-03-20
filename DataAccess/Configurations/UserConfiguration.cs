using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Presentation.Constants;

namespace Presentation.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(u => u.Id);

            builder
                .Property(u => u.UserName)
                .HasMaxLength(EntityConstants.User.Name.Max);

            builder
                .Property(u => u.Email)
                .HasMaxLength(EntityConstants.User.Email.Max);

        }

    }
}
