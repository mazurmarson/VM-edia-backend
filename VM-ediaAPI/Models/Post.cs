using System;
using System.Collections.Generic;

namespace VM_ediaAPI.Models
{
    public class Post
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Description { get; set; } 
        public DateTime CreateAt { get; set; }

        public List<Photo> Photos { get; set; }
        public List<Reaction> Reactions { get; set; }
        public List<Comment> Comments { get; set; }

    }
}