using System;
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
    public class PostController : ControllerBase
    {
        private readonly IPostRepo _repo;
        private readonly IMapper _mapper;

        public PostController(IPostRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(AddPostDto addPostDto)
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostDetails(int id)
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
            
            var postDetails = await _repo.GetPostDetailsDto(id, userId);

            return Ok(postDetails);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var post = await _repo.GetPostById(id);
            if(post.UserId == userId)
            {
                _repo.Delete(post);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePost(int id,JsonPatchDocument<UpdatePostDto> updatePostDto)
        {
            var post = _repo.GetPostById(id);
            var postToPatch = _mapper.Map<UpdatePostDto>(post);
            updatePostDto.ApplyTo(postToPatch);
            _repo.Edit(post);
            await _repo.SaveAll();

            return NoContent();


        }



    }
}