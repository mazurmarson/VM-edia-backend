namespace VM_ediaAPI.Models
{
    public class Follow
    {
        public int Id { get; set; }
        public int FollowerId { get; set; }
        public  User Follower { get; set; }
        public int FollowedUserId {get; set;}
        public  User FollowedUser {get; set;}
    }
}