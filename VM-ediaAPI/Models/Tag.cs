using System.Collections.Generic;

namespace VM_ediaAPI.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Amount { get; set; }
        public ICollection<PostTag> PostTags { get; set; }
    }
}