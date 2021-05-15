namespace VM_ediaAPI.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public virtual int UserId { get; set;}
        public bool IsMain { get; set; }
        public string Description { get; set; }
        public string UrlAdress { get; set; }

    }
}