using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
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
        private readonly IMapper _mapper;

        public UserController(IUserRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
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
            try
            {
                int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                //zalogowany ale nie obserwuje
                var userLogged = await _repo.GetUserDetails(id, userId);
                return Ok(userLogged);
            }
            catch
            {
                //Niezalogowany
                int userId = 0;
                 var user = await _repo.GetUserDetails(id, userId);
                 return Ok(user);
            }
            


        }

        // [HttpGet("followers/{id}")]
        // public async Task<IActionResult> GetUserFollowers(int id)
        // {
        //     var users = await _repo.GetUserFollowers(id);
        //     return Ok(users);
        // }

        [HttpGet("{id}/followers")]
        public async Task<IActionResult> GetUserFollowers(int id)
        {
            var users = await _repo.GetUserFollowers(id);
            return Ok(users);
        }

        
        // [HttpGet("following/{id}")]
        // public async Task<IActionResult> GetUserFollowing(int id)
        // {
        //     var users = await _repo.GetUserFollowing(id);
        //     return Ok(users);
        // }

         [HttpGet("{id}/following")]
        public async Task<IActionResult> GetUserFollowing(int id)
        {
            var users = await _repo.GetUserFollowing(id);
            return Ok(users);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdatePatch( JsonPatchDocument<UpdateUserDto> userDto)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user = await _repo.GetUserById(userId);

            var userToPatch = _mapper.Map<UpdateUserDto>(user);
            userDto.ApplyTo(userToPatch);
            _mapper.Map(userToPatch, user);
            _repo.Edit(user);
            await _repo.SaveAll();

            return NoContent();
        }
    }
}