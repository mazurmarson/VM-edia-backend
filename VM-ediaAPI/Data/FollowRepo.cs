using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VM_ediaAPI.Models;

namespace VM_ediaAPI.Data
{
    public class FollowRepo : GenRepo, IFollowRepo
    {
        private readonly DataContext _context;

        public FollowRepo(DataContext context):base(context)
        {
            _context = context;
        }

        public async Task<Follow> AddFollow(Follow follow)
        {
            await _context.AddAsync(follow);
            await _context.SaveChangesAsync();

            return follow;
        }

        public async Task<bool> FollowIsExist(int id, int loggedUserId)
        {
            bool followIsExist = await _context.Follows.Where(x => x.FollowedUserId == id && x.FollowerId == loggedUserId).AnyAsync();
            return followIsExist;
        }

        public async Task<Follow> GetFollow(int id)
        {
            var follow = await _context.Follows.FirstOrDefaultAsync(x => x.Id == id);
        
            return follow;
        }

        public Task<IEnumerable<Follow>> GetUserFollows(int userId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> UserIsExist(int id)
        {
            var userIsExist = await _context.Users.AnyAsync(x => x.Id == id);
            return userIsExist;
        }

        // public Task<IEnumerable<Follow>> GetUserFollows(int userId)
        // {
        //     var fo
        // }
    }
}