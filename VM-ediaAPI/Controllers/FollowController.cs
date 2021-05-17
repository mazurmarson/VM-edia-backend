using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VM_ediaAPI.Data;
using VM_ediaAPI.Models;

namespace VM_ediaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        private readonly IFollowRepo _repo;
        public FollowController(IFollowRepo repo)
        {
            _repo = repo;
        }
      //  [Authorize]
        [HttpPost("{followedUserId}")]
        public ActionResult FollowUser(int followedUserId)
        {
             Follow follow = new Follow
             {
                FollowerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
                 FollowedUserId = followedUserId
             };

             _repo.AddFollow(follow);
            // _repo.Add<Follow>(follow);

            
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Unfollow(int id)
        {
            
                var follow = await _repo.GetFollow(id);
                _repo.Delete(follow);
                await _repo.SaveAll();
                return NoContent();
        }

        // [HttpGet]
        // public ActionResult GetUserFollows()
        // {
        //     int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        //     var userFollows = _repo.GetUserFollow(userId);
        // }
    }
}