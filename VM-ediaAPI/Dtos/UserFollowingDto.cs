namespace VM_ediaAPI.Dtos
{
    public class UserFollowingDto
    {
        public int Id { get; set; }
         public int FollowingId {get; set;}
        public string Login { get; set; }  

        public string MainPhotoUrl {get; set;}
    }
}