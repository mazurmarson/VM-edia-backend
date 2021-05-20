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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var photo = await _repo.GetPhoto(id);
            return Ok(photo);
        }

        [HttpPost]
        public async Task<ActionResult> AddPhoto(AddPhotoDto addPhotoDto)
        {
           
            Photo photo = new Photo
            {
              //  UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
             
             //   UrlAdress = addPhotoDto.UrlAdress,
           
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

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePhoto(int id, string description)
        {
            var photo = await _repo.GetPhoto(id);
           
            _repo.Edit(photo);

            if(await _repo.SaveAll())
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}