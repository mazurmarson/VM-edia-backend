using System;
using System.Collections.Generic;

namespace VM_ediaAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }  
        public string Mail { get; set; }    
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Description { get; set; }
        public virtual List<Photo> Photos {get; set;}
        public virtual List<Follow> Followers {get; set;}
    }
}