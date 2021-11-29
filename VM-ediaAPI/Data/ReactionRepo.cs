using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VM_ediaAPI.Exceptions;
using VM_ediaAPI.Models;

namespace VM_ediaAPI.Data
{
    public class ReactionRepo : GenRepo, IReactionRepo
    {
        private readonly DataContext _context;
        public ReactionRepo(DataContext context):base(context)
        {
            _context = context;
        }

        public async Task<Reaction> GetReactionById(int id)
        {
            var reaction = await _context.Reactions.FirstOrDefaultAsync(x => x.Id == id);
         
            return reaction;
        }
    }
}