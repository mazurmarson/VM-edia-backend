using System.Threading.Tasks;
using VM_ediaAPI.Models;

namespace VM_ediaAPI.Data
{
    public class UserRepo : GenRepo, IUserRepo
    {
        private readonly DataContext _context;
        public UserRepo(DataContext context):base(context)
        {
            _context = context;
        }

        public async Task<User> Register(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}