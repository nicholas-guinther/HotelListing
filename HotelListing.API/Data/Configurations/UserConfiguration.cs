using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.API.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    private readonly PasswordHasher<User> hasher = new PasswordHasher<User>();
    
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasData(
            new User
            {
                Id = "1",
                FirstName = "Admin",
                LastName = "User",
                Email = "admin@email.com",
                NormalizedEmail = "ADMIN@EMAIL.COM",
                UserName = "admin@email.com",
                NormalizedUserName = "ADMIN@EMAIL.COM",
                PasswordHash = hasher.HashPassword(null, "P@ssword1")
            },
            new User
            {
                Id = "2",
                FirstName = "Standard",
                LastName = "User",
                Email = "standard@email.com",
                NormalizedEmail = "STANDARD@EMAIL.COM",
                UserName = "standard@email.com",
                NormalizedUserName = "STANDARD@EMAIL.COM",
                PasswordHash = hasher.HashPassword(null, "P@ssword1")
            });
    }
}