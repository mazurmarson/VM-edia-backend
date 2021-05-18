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
                Description = registerUserDto.Description,
                MainPhotoUrl = registerUserDto.MainPhotoUrl
                

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

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<User> EditUser(User user, string newPassword)
        {
            
            user.PasswordHash = _passwordHasher.HashPassword(user, newPassword);

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public async Task<DetailsUserDto> GetUserDetails(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id ==id);
            int followers = await _context.Follows.CountAsync(x => x.FollowedUserId == id);
            int following = await _context.Follows.CountAsync(x => x.FollowerId == id);
            var photos = await _context.Photos.Where(x => x.UserId == id).Select(x => new PhotoUserDto()
            {
                Id = x.Id,
                UserId = x.UserId,
         
                UrlAdress = x.UrlAdress,
     

            }).ToListAsync();

            DetailsUserDto detailsUserDto = new DetailsUserDto
            {
                Id = user.Id,
                Login = user.Login,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Description = user.Description,
                MainPhotoUrl = user.MainPhotoUrl,
                AmountFollowers = followers,
                AmoutnFollowing = following,
                Photos = photos
            };

            return detailsUserDto;
        }

        public async Task<IEnumerable<UserFollowersDto>> GetUserFollowers(int id)
        {
            var users = await _context.Follows.Where(x => x.FollowedUserId == id).Include(x => x.Follower).Select(x => new UserFollowersDto()
            {
                Id = x.Id,
                FollowerId = x.FollowerId,
                Login = x.Follower.Login,
                MainPhotoUrl = x.Follower.MainPhotoUrl
            }).ToListAsync();
            return users;
        }

        public async Task<IEnumerable<UserFollowingDto>> GetUserFollowing(int id)
        {
            var users = await _context.Follows.Where(x => x.FollowerId == id).Include(x => x.FollowedUser).Select(x => new UserFollowingDto()
            {
                Id = x.Id,
                FollowingId = x.FollowedUserId,
                Login = x.FollowedUser.Login,
                MainPhotoUrl = x.FollowedUser.MainPhotoUrl
            }).ToListAsync();

            return users;
        }

        //Obsluga przykłądowego bledu NotFound w metodzie GET
        //If(smomething is null)
        //throw new NotFoundException("Something not found")
    }
}