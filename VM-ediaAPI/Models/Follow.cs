namespace VM_ediaAPI.Models
{
    public class Follow
    {
        public int Id { get; set; }
        public virtual int FollowerId { get; set; }
        public virtual int FollowedUserId {get; set;}
    }
}