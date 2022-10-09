using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cripto_Api.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            string saltuser = BCrypt.Net.BCrypt.GenerateSalt(12);

            builder.HasData(
                new User
                {   
                    Id = 1,
                    Username = "Eima",
                    Password = BCrypt.Net.BCrypt.HashPassword("admin" +salt),
                    Salt = salt,
                    Role = "admin"
                },
                new User
                {
                    Id = 2,
                    Username = "Eima1",
                    Password = BCrypt.Net.BCrypt.HashPassword("admin" +saltuser),
                    Salt = saltuser,
                    Role= "user"
                }
            );

        }
    }
}
