namespace VM_ediaAPI.Models
{
    public class Reaction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
      //  public bool IsActive { get; set; }
    }
}