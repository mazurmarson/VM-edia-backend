namespace VM_ediaAPI.Dtos
{
    public class FollowUserReturn
    {
        public int Id { get; set; }
        public string Login { get; set; }  
        public string Mail { get; set; }    
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        // public DateTime DateOfBirth { get; set; }
        // public string Description { get; set; }
        // public  List<Photo> Photos {get; set;}
        // public  List<Follow> Followers {get; set;}
        // public  List<Follow> FollowedUsers {get; set;}
    }
}