using Cripto_Api.Contract;
using Cripto_Api.Data;
using Cripto_Api.Models.User;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Cripto_Api.Repository
{
    public class UserRepository : GenericRepository<User> , IUserContract
    {
        private readonly CriptoAPIDbContext _context;
        private readonly IConfiguration _configuration;
        public UserRepository(IConfiguration configuration,CriptoAPIDbContext database) : base(database)
        {
            _context = database;
            _configuration = configuration;
        }
        public async Task<User> GetUserById(int id)
        {
            return await _context.Users
             .Include(q => q.Wallets)
             .ThenInclude(q => q.Coins)
             .Where(x => x.Id == id)
             .FirstOrDefaultAsync();
        }
        public async Task<User> UpdatePass(UpdateUserDto updateUserDto, User user)
        {
            if (updateUserDto.Password != null && updateUserDto.OllPass != null && BCrypt.Net.BCrypt.Verify(updateUserDto.Password += user.Salt, user.Password))
            {
                updateUserDto.OllPass += user.Salt;
                user.Password =  BCrypt.Net.BCrypt.HashPassword(updateUserDto.OllPass);

                return user;
            }
            return null;
        }

        public async Task<UserResponseTokenDto> Login(LoginUserDto loginUserDto)
        {
            var personTest = await _context.Users
                .Where(x => x.Username == loginUserDto.Username)
                .FirstOrDefaultAsync();

            if (personTest != null && BCrypt.Net.BCrypt.Verify(loginUserDto.Password += personTest.Salt, personTest.Password))
            {
                var token = await GenerateToken(personTest);

                //await _context.Users.AddAsync(token);
                return new UserResponseTokenDto
                {
                    Token = token,
                    Id = personTest.Id
                };
            }
            return null;
        }
        public async Task<User> Register(User user)
        {
            if(await _database.Users
                .Where(x => x.Username.ToUpper() == user.Username.ToUpper())
                .FirstOrDefaultAsync() != null)
            {
                return null;
            }
            user.Salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            user.Password += user.Salt;
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Role = "user";

            await _database.Users.AddAsync(user);
            await _database.SaveChangesAsync();
            return user;
        }

        private async Task<string> GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:Key"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //var roles =  await _context.Users.FindAsync(user);

            var claims = new [] //difine user object
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
            };
            //.Union(roles);

            var token = new JwtSecurityToken(               //token object
                issuer:_configuration["JWTSettings:Issuer"], 
                audience:_configuration["JWTSettings:Audience"],
                claims:claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JWTSettings:DurationInMinutes"])),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
