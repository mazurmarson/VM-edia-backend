using System.Collections.Generic;
using VM_ediaAPI.Models;

namespace VM_ediaAPI.Dtos
{
    public class AddPostDto
    {
        public string Description { get; set; } 


        public List<Photo> Photos { get; set; }

    }
}