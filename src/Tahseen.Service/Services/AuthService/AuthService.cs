using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tahseen.Data.IRepositories;
using Tahseen.Domain.Entities;
using Tahseen.Service.DTOs.Login;
using Tahseen.Service.DTOs.Users.User;
using Tahseen.Service.Exceptions;
using Tahseen.Service.Interfaces.IAuthService;
using Tahseen.Service.Interfaces.IUsersService;

namespace Tahseen.Service.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IRepository<User> _userRepository;

        public AuthService(IRepository<User> userRepository, IConfiguration configuration)
        {
            this._userRepository = userRepository; 
            this._configuration = configuration;
        }

        public async Task<LoginForResultDto> AuthenticateAsync(LoginDto dto)
        {
            var user = await _userRepository.SelectAll()
                .Where(u => u.UserName == dto.UserName && u.Password == dto.Password && u.IsDeleted == false)
                .FirstOrDefaultAsync();
            if(user == null)
            {
                throw new TahseenException(400, "UserName or Password is Incorrect");
            }

            return new LoginForResultDto
            {
                Token = GenerateToken(user)
            };
        }


        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                 new Claim("Id", user.Id.ToString()),
                 new Claim(ClaimTypes.Email, user.EmailAddress.ToString()),
                 new Claim("LibraryId", user.LibraryId.ToString()), 
                 new Claim(ClaimTypes.Role, user.Role.ToString()), 

                }),
                Audience = _configuration["JWT:Audience"],
                Issuer = _configuration["JWT:Issuer"],
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JWT:Expire"])),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
