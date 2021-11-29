using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using VM_ediaAPI.Data;
using VM_ediaAPI.Dtos;
using VM_ediaAPI.Models;

namespace VM_ediaAPI.Controllers
{
    [Route("api/user/{userId}/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepo _repo;
        private readonly IMapper _mapper;

        public CommentController(ICommentRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        [Authorize]
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
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _repo.GetCommentById(id);
            if (comment == null)
            {
                return NotFound();
            }
            if (int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value) != comment.UserId)
            {
                return StatusCode(403);
            }
            _repo.Delete(comment);
            await _repo.SaveAll();
            return NoContent();
        }

        [Authorize]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateComment(int id, JsonPatchDocument<UpdateCommentDto> commentDto)
        {
            var comment = await _repo.GetCommentById(id);
            if (comment == null)
            {
                return NotFound();
            }
            if (int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value) != comment.UserId)
            {
                return StatusCode(403);
            }
            var commentToPatch = _mapper.Map<UpdateCommentDto>(comment);
            commentDto.ApplyTo(commentToPatch);
            _mapper.Map(commentToPatch, comment);
            _repo.Edit(comment);
            await _repo.SaveAll();

            return NoContent();
        }
    }
}