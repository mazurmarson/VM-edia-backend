using System.Threading.Tasks;

namespace VM_ediaAPI.Data
{
    public class GenRepo : IGenRepo
    {
        private readonly DataContext _context;

        public GenRepo(DataContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T enitty) where T : class
        {
            _context.Remove(enitty);
        }

        public void Edit<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}