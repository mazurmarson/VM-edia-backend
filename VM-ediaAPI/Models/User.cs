using System;
using System.Collections.Generic;

namespace VM_ediaAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }  
        public string Mail { get; set; }    
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Description { get; set; }
        public string Role {get; set;} ="user";
        public string MainPhotoUrl {get; set;}
        public  List<Post> Posts {get; set;}
        public  List<Follow> Followers {get; set;}
        public  List<Follow> FollowedUsers {get; set;}
    }
}