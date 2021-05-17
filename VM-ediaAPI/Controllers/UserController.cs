using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VM_ediaAPI.Data;
using VM_ediaAPI.Dtos;
using VM_ediaAPI.Models;

namespace VM_ediaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _repo;

        public UserController(IUserRepo repo)
        {
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            await _repo.Register(registerUserDto);
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            string token = await _repo.GenerateJwt(dto);
            return Ok(token);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(EditUserDto editUserDto)
        {
            var userToEdit = new User
            {
                Id = editUserDto.Id,
                Login = editUserDto.Login,
                Mail = editUserDto.Mail,
                FirstName = editUserDto.FirstName,
                LastName = editUserDto.LastName,
                DateOfBirth = editUserDto.DateOfBirth,
                Description = editUserDto.Description,
                MainPhotoUrl = editUserDto.MainPhotoUrl
            };

            var editedUser = await _repo.EditUser(userToEdit, editUserDto.Password);

            return StatusCode(201);
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repo.GetUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUserById(id);
            return Ok(user);
        }
    }
}