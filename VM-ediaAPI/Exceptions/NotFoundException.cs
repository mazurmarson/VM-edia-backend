using System;

namespace VM_ediaAPI.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message): base(message)
        {
            
        }
    }
}