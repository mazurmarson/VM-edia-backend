namespace VM_ediaAPI.Data
{
    public class CommentRepo : GenRepo, ICommentRepo
    {
        private readonly DataContext _context;

        public CommentRepo(DataContext context): base(context)
        {
            _context = context;
        }
    }
}