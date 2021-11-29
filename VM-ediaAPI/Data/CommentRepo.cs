using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VM_ediaAPI.Models;

namespace VM_ediaAPI.Data
{
    public class CommentRepo : GenRepo, ICommentRepo
    {
        private readonly DataContext _context;

        public CommentRepo(DataContext context): base(context)
        {
            _context = context;
        }

        public async Task<Comment> GetCommentById(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
            return comment;
        }
    }
}