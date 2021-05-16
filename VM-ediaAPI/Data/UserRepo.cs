using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using VM_ediaAPI.Dtos;
using VM_ediaAPI.Exceptions;
using VM_ediaAPI.Models;

namespace VM_ediaAPI.Data
{
    public class UserRepo : GenRepo, IUserRepo
    {
        private readonly DataContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        public UserRepo(DataContext context, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings):base(context)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }

        public async Task<RegisterUserDto> Register(RegisterUserDto registerUserDto)
        {
            var user = new User() 
            {
                FirstName = registerUserDto.FirstName,
                LastName = registerUserDto.LastName,
                Login = registerUserDto.Login,
                Mail = registerUserDto.Mail,
                DateOfBirth = registerUserDto.DateOfBirth,
                Description = registerUserDto.Description
                

            };
            var hashedPassword = _passwordHasher.HashPassword(user, registerUserDto.Password);
            user.PasswordHash = hashedPassword;
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return registerUserDto;
        }

        public async Task<string> GenerateJwt(LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Mail == dto.Mail);

            if(user is null)
            {
                throw new BadRequestException("Inavalid username or password");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if(result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Inavalid username or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                //Tu bedzie można dodać role
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, _authenticationSettings.JwtIssuer, claims, expires: expires, signingCredentials:cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        //Obsluga przykłądowego bledu NotFound w metodzie GET
        //If(smomething is null)
        //throw new NotFoundException("Something not found")
    }
}