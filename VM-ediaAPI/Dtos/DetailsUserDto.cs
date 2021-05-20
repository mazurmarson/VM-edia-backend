using System;
using System.Collections.Generic;
using VM_ediaAPI.Models;

namespace VM_ediaAPI.Dtos
{
    public class DetailsUserDto
    {
        public int Id { get; set; }
        public string Login { get; set; }  
 //       public string Mail { get; set; }    
        public string FirstName { get; set; }
        public string LastName { get; set; }
     //   public DateTime DateOfBirth { get; set; }
        public string Description { get; set; }
        public string MainPhotoUrl {get; set;}
        public int AmountFollowers {get; set;}
        public int AmoutnFollowing { get; set;}
        public int FollowingId {get; set;}

        public  List<UserDetailsPostDto> Posts {get; set;}



    }
}