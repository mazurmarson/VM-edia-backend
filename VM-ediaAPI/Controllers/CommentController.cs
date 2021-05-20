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
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepo _repo;

        public CommentController(ICommentRepo repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public async Task<ActionResult> AddComment(AddCommentDto addCommentDto)
        {
            Comment comment = new Comment
            {
                PostId = addCommentDto.PostId,
                UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
                Content = addCommentDto.Content,
                CreatedAt = DateTime.Now
            };
            _repo.Add(comment);
            await _repo.SaveAll();

            return StatusCode(201);
        }
    }
}