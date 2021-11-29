using System.Threading.Tasks;
using VM_ediaAPI.Dtos;
using VM_ediaAPI.Models;

namespace VM_ediaAPI.Data
{
    public interface IPostRepo : IGenRepo
    {
         Task<PostDetailsDto> GetPostDetailsDto(int postId, int userId);
         Task<Post> GetPostById(int postId);
    }
}