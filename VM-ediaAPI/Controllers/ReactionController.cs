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
    [Route("api/[controller]")]
    [ApiController]
    public class ReactionController : ControllerBase
    {
        private readonly IReactionRepo _repo;
        private readonly IMapper _mapper;

        public ReactionController(IReactionRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddReaction(AddReactionDto addReactionDto)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            
            Reaction reaction = new Reaction
            {
                PostId = addReactionDto.PostId,
                UserId = userId,
                IsPositive = addReactionDto.IsPositive
            };


            _repo.Add(reaction);
            await _repo.SaveAll();

            return StatusCode(201);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReaction(int id)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var reaction = await _repo.GetReactionById(id);
            if(reaction == null)
            {
                return BadRequest();
            }
            if(reaction.UserId != userId)
            {
                return StatusCode(403);
            }
            _repo.Delete(reaction);

            await _repo.SaveAll();

            return NoContent();
        }

        [Authorize]
        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateReaction(int id, JsonPatchDocument<UpdateReactionDto> updateReactionDto)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var reaction = await _repo.GetReactionById(id);
            if(userId != reaction.UserId)
            {
                return StatusCode(403);
            }
            var reactionToPatch = _mapper.Map<UpdateReactionDto>(reaction);
            updateReactionDto.ApplyTo(reactionToPatch);
            _mapper.Map(reactionToPatch, reaction);
            _repo.Edit(reaction);
            await _repo.SaveAll();

            return NoContent();
        }
    }
}