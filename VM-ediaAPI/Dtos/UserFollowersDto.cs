namespace VM_ediaAPI.Dtos
{
    public class UserFollowersDto
    {
         public int Id { get; set; }
         public int FollowerId {get; set;}
        public string Login { get; set; }  

        public string MainPhotoUrl {get; set;}
        
    }
}