using System.Threading.Tasks;
using VM_ediaAPI.Dtos;

namespace VM_ediaAPI.Data
{
    public interface IPostRepo : IGenRepo
    {
         Task<PostDetailsDto> GetPostDetailsDto(int postId, int userId);
    }
}