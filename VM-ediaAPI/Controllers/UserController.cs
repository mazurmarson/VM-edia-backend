using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using VM_ediaAPI.Data;
using VM_ediaAPI.Dtos;
using VM_ediaAPI.Helpers;
using VM_ediaAPI.Models;
using VM_ediaAPI.Validators;

namespace VM_ediaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _repo;
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;

        public UserController(IUserRepo repo, IMapper mapper, DataContext dataContext)
        {
            _repo = repo;
            _mapper = mapper;
            _dataContext = dataContext;

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

        // [HttpPut("update")]
        // public async Task<IActionResult> Update(EditUserDto editUserDto)
        // {
        //     var userToEdit = new User
        //     {
        //         Id = editUserDto.Id,
        //         Login = editUserDto.Login,
        //         Mail = editUserDto.Mail,
        //         FirstName = editUserDto.FirstName,
        //         LastName = editUserDto.LastName,
        //         DateOfBirth = editUserDto.DateOfBirth,
        //         Description = editUserDto.Description,
        //         MainPhotoUrl = editUserDto.MainPhotoUrl
        //     };

        //     var editedUser = await _repo.EditUser(userToEdit, editUserDto.Password);

        //     return StatusCode(201);
        // }

        // [HttpPatch("{id}")]
        // public async Task<IActionResult> UpdatePatch(int id, JsonPatchDocument<EditUserDto> editUserDto)
        // {
        //     var user =  await _repo.GetUserById(id);
        //     var userToPatch = _mapper.Map<EditUserDto>(user);
        //     editUserDto.ApplyTo(userToPatch);
        //     _repo.Edit(user);
        //     await _repo.SaveAll();

        //     return NoContent();
        // } 
        // [HttpGet]
        // public async Task<IActionResult> GetUsers()
        // {
        //     var users = await _repo.GetUsers();
        //     return Ok(users);
        // }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] PageParameters pageParameters, string searchString)
        {
            
            if(searchString == null)
            {
                searchString ="";
            }
            var users = await _repo.GetSearchedAndSortedUsers(pageParameters, searchString);
            Pagger<UsersDisplayDto> usersToReturn = new Pagger<UsersDisplayDto>(users);
            return Ok(usersToReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id,[FromQuery]  PageParameters pageParameters)
        {
            int userId;
            if(User.Identity.IsAuthenticated)
            {
                userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            }
            else
            {
                userId = 0;
            }

                 var user = await _repo.GetUserDetails(id, userId, pageParameters);
                Pagger<UserDetailsPostDto> postToReturn = new Pagger<UserDetailsPostDto>(user.Posts);
                DetailsUserPaggedDto detailsUserPaggedDto = new DetailsUserPaggedDto()
                {
                    Login = user.Login,
                    Posts = postToReturn,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Description = user.Description,
                    MainPhotoUrl = user.MainPhotoUrl,
                    AmountFollowers = user.AmountFollowers,
                    AmoutnFollowing = user.AmoutnFollowing,
                    FollowingId = user.FollowingId
                };

                if(user == null)
                {
                    return NotFound();
                }
                 return Ok(detailsUserPaggedDto);

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
            if(!users.Any())
            {
                return NotFound();
            }
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
            if(!users.Any())
            {
                return NotFound();
            }
            return Ok(users);
        }
        [Authorize]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePatch(int id, JsonPatchDocument<UpdateUserDto> userDto)
        {

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if(id != userId)
            {
                return Unauthorized();
            }
            
            var user = await _repo.GetUserById(id);
            // var test = userDto.Operations.Where(x => x.path.Equals("/login") || x.path.Equals("/description") || x.path.Equals("/mainPhotoUrl") ).Any();
            // if(test)
            // {
            //     return BadRequest();
            // }
            var userToPatch = _mapper.Map<UpdateUserDto>(user);
            userDto.ApplyTo(userToPatch);
       //     var validationResult = new UpdateUserDtoValidator(_dataContext).Validate(userToPatch);
            _mapper.Map(userToPatch, user);
            _repo.Edit(user);
            await _repo.SaveAll();

            return NoContent();
        }

        [Authorize]
        [HttpGet("wall")]
        public async Task<IActionResult> GetWall()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var wall = await _repo.GetWall(userId);

            return Ok(wall);
        }
    }
}