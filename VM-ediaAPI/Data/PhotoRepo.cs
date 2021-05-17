using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VM_ediaAPI.Models;

namespace VM_ediaAPI.Data
{
    public class PhotoRepo : GenRepo, IPhotoRepo
    {
        private readonly DataContext _context;

        public PhotoRepo(DataContext context):base(context)
        {
            _context = context;
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(x => x.Id == id);
            return photo;
        }
    }
}