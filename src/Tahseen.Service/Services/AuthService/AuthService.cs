using System.Text;
using System.Security.Claims;
using Tahseen.Service.Helpers;
using Tahseen.Domain.Entities;
using Tahseen.Data.IRepositories;
using Tahseen.Service.DTOs.Login;
using Tahseen.Service.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Tahseen.Domain.Entities.Librarians;  // Include Librarian entity
using Tahseen.Service.Interfaces.IAuthService;
using Tahseen.Domain.Enums;

namespace Tahseen.Service.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Librarian> _librarianRepository;  // Add Librarian repository

        public AuthService(
            IRepository<User> userRepository, 
            IRepository<Librarian> librarianRepository,
            IConfiguration configuration)
        {
            this._userRepository = userRepository;
            this._librarianRepository = librarianRepository;
            this._configuration = configuration;
        }

        public async Task<LoginForResultDto> AuthenticateAsync(LoginDto dto)
        {
            // Check if the credentials belong to a user
            var user = await _userRepository.SelectAll()
                .Where(u => u.PhoneNumber == dto.PhoneNumber && u.IsDeleted == false && u.Role == Roles.User)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (user != null)
            {
                // Authenticate user
                var hashedPassword = PasswordHelper.Verify(dto.Password, user.Salt, user.Password);
                if (hashedPassword == false)
                {
                    throw new TahseenException(400, "UserName or Password is Incorrect");
                }

                return new LoginForResultDto
                {
                    Token = GenerateToken(user)
                };
            }

            // If not a user, check if the credentials belong to a librarian
            var librarian = await _librarianRepository.SelectAll()
                .Where(l => l.PhoneNumber == dto.PhoneNumber && l.IsDeleted == false && l.Roles == Roles.Librarian)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (librarian != null)
            {
                // Authenticate librarian
                var hashedPassword = PasswordHelper.Verify(dto.Password, librarian.Salt, librarian.Password);
                if (hashedPassword == false)
                {
                    throw new TahseenException(400, "UserName or Password is Incorrect");
                }

                return new LoginForResultDto
                {
                    Token = GenerateLibrarianToken(librarian)
                };
            }

            throw new TahseenException(400, "UserName or Password is Incorrect");
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
            new Claim("LibraryBranchId", user.LibraryBranchId.ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString()),  // Use the role passed as an argument
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

        // Additional method for generating token for Librarian
        private string GenerateLibrarianToken(Librarian librarian)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim("Id", librarian.Id.ToString()),
            new Claim(ClaimTypes.Email, librarian.Email.ToString()),
            new Claim("LibraryBranchId", librarian.LibraryBranchId.ToString()),
            new Claim(ClaimTypes.Role, librarian.Roles.ToString()),  // Use the role passed as an argument
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
