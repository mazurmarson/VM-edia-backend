using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VM_ediaAPI.Data;
using VM_ediaAPI.Dtos;
using VM_ediaAPI.Models;

namespace VM_ediaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepo _repo;

        public PostController(IPostRepo repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public async Task<IActionResult> AddPhoto(AddPostDto addPostDto)
        {
            Post post = new Post
            {
                UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
                Description = addPostDto.Description,
                CreateAt = DateTime.Now,
                Photos = addPostDto.Photos
            };
            _repo.Add(post);
            await _repo.SaveAll();

            return StatusCode(201);
        }


        
    }
}