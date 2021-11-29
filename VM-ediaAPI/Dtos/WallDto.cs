using System;
using System.Collections.Generic;
using VM_ediaAPI.Models;

namespace VM_ediaAPI.Dtos
{
    public class WallDto
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public string MainPhotoUrl { get; set; }
        public int PostId { get; set; }
        public string Description { get; set; }
        public DateTime CreateAt { get; set; }
        public List<PostPhotoDto> Photos { get; set; }
         public List<PostCommentDto> Comments { get; set; }
        public int PositiveReactions { get; set; }
        public int NegativeReactions { get; set; }
    
    }
}