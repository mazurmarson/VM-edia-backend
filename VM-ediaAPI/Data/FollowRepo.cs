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

        public async Task<Follow> GetFollow(int id)
        {
            var follow = await _context.Follows.FirstOrDefaultAsync(x => x.Id == id);
        
            return follow;
        }

        public Task<IEnumerable<Follow>> GetUserFollows(int userId)
        {
            throw new System.NotImplementedException();
        }

        // public Task<IEnumerable<Follow>> GetUserFollows(int userId)
        // {
        //     var fo
        // }
    }
}