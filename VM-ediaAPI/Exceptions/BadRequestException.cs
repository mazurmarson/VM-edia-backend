using System;

namespace VM_ediaAPI.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message): base(message)
        {
            
        }
    }
}