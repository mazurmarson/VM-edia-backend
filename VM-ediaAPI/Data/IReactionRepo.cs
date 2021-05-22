using System.Threading.Tasks;
using VM_ediaAPI.Models;

namespace VM_ediaAPI.Data
{
    public interface IReactionRepo : IGenRepo
    {
         Task<Reaction> GetReactionById(int id);
    }
}