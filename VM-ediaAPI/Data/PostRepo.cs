namespace VM_ediaAPI.Data
{
    public class PostRepo : GenRepo, IPostRepo
    {
        private readonly DataContext _context;
        public PostRepo(DataContext context):base(context)
        {
            _context = context;
        }
    }
}