using System.Threading.Tasks;
using VM_ediaAPI.Models;

namespace VM_ediaAPI.Data
{
    public interface IUserRepo : IGenRepo
    {
         Task<User> Register(User user);
    }
}