using System.Collections.Generic;
using System.Threading.Tasks;
using VM_ediaAPI.Models;

namespace VM_ediaAPI.Data
{
    public interface ITagRepo : IGenRepo
    {
         Task<List<PostTag>> GetTagsAndUpdateAmmountAdding(string description);
         Task<bool> TagAmmountDecrementationByPostId(int postId);
         Task<bool> TagAmmountDecrementationUpdateByPostId(int postId);
         Task<List<PostTag>> UpdatePostTags(string description, int postId);
         
    }
}