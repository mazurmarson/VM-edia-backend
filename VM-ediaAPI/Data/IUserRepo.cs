using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using VM_ediaAPI.Dtos;
using VM_ediaAPI.Models;

namespace VM_ediaAPI.Data
{
    public interface IUserRepo : IGenRepo
    {
         Task<RegisterUserDto> Register(RegisterUserDto registerUserDto);
         Task<string> GenerateJwt(LoginDto dto);
    }
}