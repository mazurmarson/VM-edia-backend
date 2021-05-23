using System.Threading.Tasks;
using VM_ediaAPI.Models;

namespace VM_ediaAPI.Data
{
    public interface ICommentRepo : IGenRepo
    {
         Task<Comment> GetCommentById(int id);
    }
}