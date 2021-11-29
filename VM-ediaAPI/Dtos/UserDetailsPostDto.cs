namespace VM_ediaAPI.Dtos
{
    public class UserDetailsPostDto
    {
         public int PhotoId { get; set; }
         public int PostId {get; set;}
         public bool MoreThanOne { get; set; }

        public string UrlAdress { get; set; }
    }
}