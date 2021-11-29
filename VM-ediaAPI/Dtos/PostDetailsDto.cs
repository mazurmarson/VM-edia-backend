using System;
using System.Collections.Generic;
using VM_ediaAPI.Models;

namespace VM_ediaAPI.Dtos
{
    public class PostDetailsDto
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string AuthorLogin { get; set; }
        public string Description { get; set; } 
        public DateTime CreateAt { get; set; }
      //  public bool UserReaction { get; set; }

        public BoolDto UserReaction {get; set;} 

        public List<PostPhotoDto> Photos { get; set; }

        public PostReactionDto Reactions { get; set; }

        public List<PostCommentDto> Comments { get; set; }


    }
}