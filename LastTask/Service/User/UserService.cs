using LastTask.Model;
using LastTask.Table;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;


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
                PasswordHash=passwordHash,
                CreatedAt=DateTime.Now
            };
           
                var result = await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return user;
           
            

        }
        public string CreateToken(Table.User user)
        {
            List<Claim> claims = new List<Claim> {
                new Claim("UserId", user.UserId.ToString()),
                new Claim("name", user.Username),
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

        public async Task<Table.User?> GetUser(string username) {
            
                return await _context.Users.FirstOrDefaultAsync(x => x.Username == username);   
        }
        public async Task<Table.Profile> createProfile(int id, UserModel model)
        {
            var profile = new Profile
            {
                UserId = id,
                FullName = model.fullname,
                Bio = model.bio,
                ProfileImageURL = model.imageurl
            };
           await _context.AddAsync(profile);
            await _context.SaveChangesAsync();
            return profile;
            
        }
           public int? GetCurrentLoggedIn()
           {
            var id = _httpContextAccessor.HttpContext.Session.GetInt32("UserId");
            return id;
           }
        
           public void setsessionvalue(Table.User user)
        {
            _httpContextAccessor.HttpContext.Session.SetInt32("UserId", user.UserId);
            _httpContextAccessor.HttpContext.Session.SetString("name", user.Username);
        }



    }

}
