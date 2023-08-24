using LastTask.Model;
using LastTask.Table;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LastTask.Service.User
{
    public class UserService:IUserService
    {
        private readonly AplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(AplicationDbContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Table.User> AddUser(UserRigModel m)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(m.password);

            var user = new Table.User()
            {
                Username=m.username,
                Email=m.email,
                PasswordHash=passwordHash,
                CreatedAt=DateTime.Now
            };
           
                var result = await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return user;
           
            

        }
        public string CreateToken(string userName)
        {
            List<Claim> claims = new List<Claim> {
                new Claim("name", userName),
                new Claim(ClaimTypes.Role, "User"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
