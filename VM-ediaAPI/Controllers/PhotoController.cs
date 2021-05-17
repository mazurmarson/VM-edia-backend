using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VM_ediaAPI.Data;
using VM_ediaAPI.Dtos;
using VM_ediaAPI.Models;

namespace VM_ediaAPI.Controllers
{
        [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoRepo _repo;

        public PhotoController(IPhotoRepo repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public async Task<ActionResult> AddPhoto(AddPhotoDto addPhotoDto)
        {
           
            Photo photo = new Photo
            {
                UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
                Description = addPhotoDto.Description,
                UrlAdress = addPhotoDto.UrlAdress,
                CreatedAt = DateTime.Now
            };

            _repo.Add(photo);
            await _repo.SaveAll();
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePhoto(int id)
        {
            var photo = await _repo.GetPhoto(id);
            _repo.Delete(photo);
            await _repo.SaveAll();

            return NoContent();
        }
    }
}