using System;

namespace VM_ediaAPI.Dtos
{
    public class PostCommentDto
    {
         public int CommentId { get; set; }
        public int UserId { get; set; }
        public string UserLogin {get; set;}
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}