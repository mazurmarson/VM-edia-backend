using System.Threading.Tasks;
using VM_ediaAPI.Models;

namespace VM_ediaAPI.Data
{
    public interface IPhotoRepo : IGenRepo
    {
        
        Task<Photo> GetPhoto(int id);
        
    }
}