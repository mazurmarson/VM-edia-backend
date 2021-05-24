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
    public class ReactionController : ControllerBase
    {
        private readonly IReactionRepo _repo;

        public ReactionController(IReactionRepo repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public async Task<ActionResult> AddReaction(AddReactionDto addReactionDto)
        {
            Reaction reaction = new Reaction
            {
                PostId = addReactionDto.PostId,
                UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)
            };

             if(!ModelState.IsValid)
             {
                 return BadRequest("Coś poszło nie tak");
             }

            _repo.Add(reaction);
            await _repo.SaveAll();

            return StatusCode(201);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReaction(int id)
        {
            var reaction = _repo.GetReactionById(id);
            _repo.Delete(reaction);

            await _repo.SaveAll();

            return NoContent();
        }
    }
}