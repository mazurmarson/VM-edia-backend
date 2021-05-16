using System;

namespace VM_ediaAPI.Dtos
{
    public class RegisterUserDto
    {

        public string Login { get; set; }  
        public string Mail { get; set; }    
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Description { get; set; }

    }
}