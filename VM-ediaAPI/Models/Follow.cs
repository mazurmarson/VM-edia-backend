namespace VM_ediaAPI.Models
{
    public class Follow
    {
        public int Id { get; set; }
        public int FollowerId { get; set; }
        public virtual User Follower { get; set; }
        public int FollowedUserId {get; set;}
        public virtual User FollowedUser {get; set;}
    }
}