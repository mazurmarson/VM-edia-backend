using System;

namespace VM_ediaAPI.Models
{
    public class Photo
    {
        public int Id { get; set; }

        public string UrlAdress { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
  //      public DateTime CreatedAt {get; set;}
 //       public string Description { get; set; }
    }
}