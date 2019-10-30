using api.Helpers;
using api.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace api.Services
{

    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
    }
    public class UserService : IUserService
    {

        private List<User> _users = new List<User>
        {
            new User { Id = "1", Name = "Jack Daniels", Email = "test@test.com",  Password = "test", Role = "none" },
            new User { Id = "2", Name = "Morgan Peters", Email = "test2@test.com",  Password = "test", Role = "admin" }
        };

        private readonly AppSettings _appSettings;
        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public User Authenticate(string username, string password)
        {
            var user = _users.SingleOrDefault(x => x.Email == username && x.Password == password);

            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //Subject = new ClaimsIdentity(new Claim[]
                //{
                //    new Claim(ClaimTypes.Name, user.Id.ToString())
                //}),
                Subject = new ClaimsIdentity(new List<Claim> {
                new Claim("userid", user.Id.ToString()),
                new Claim("role", user.Role),
                new Claim("name", user.Name),
            }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }
    }
}
