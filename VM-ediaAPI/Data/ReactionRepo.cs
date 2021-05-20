namespace VM_ediaAPI.Data
{
    public class ReactionRepo : GenRepo, IReactionRepo
    {
        private readonly DataContext _context;
        public ReactionRepo(DataContext context):base(context)
        {
            _context = context;
        }

        
    }
}