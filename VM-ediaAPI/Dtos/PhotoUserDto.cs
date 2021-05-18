using System;

namespace VM_ediaAPI.Dtos
{
    public class PhotoUserDto
    {
        public int Id { get; set; }
        public  int UserId { get; set;}
        public string Description { get; set; }
        public string UrlAdress { get; set; }
        public DateTime CreatedAt {get; set;}
    }
}