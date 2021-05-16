using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using VM_ediaAPI.Dtos;
using VM_ediaAPI.Models;

namespace VM_ediaAPI.Data
{
    public class UserRepo : GenRepo, IUserRepo
    {
        private readonly DataContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        public UserRepo(DataContext context, IPasswordHasher<User> passwordHasher):base(context)
        {
            _context = context;
            _passwordHasher = passwordHasher;
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

        // public string GenerateJwt(LoginDto dto)
        // {
        //     var user = _context.Users.FirstOrDefault(x => x.Mail == dto.Mail);

        //     if(user is null)
        //     {
        //         throw new BadReqyuest
        //     }
        // }

        //Obsluga przykłądowego bledu NotFound w metodzie GET
        //If(smomething is null)
        //throw new NotFoundException("Something not found")
    }
}