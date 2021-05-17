using System;

namespace VM_ediaAPI.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public  int UserId { get; set;}
        public virtual User User {get; set;}
        public string Description { get; set; }
        public string UrlAdress { get; set; }
        public DateTime CreatedAt {get; set;}

    }
}