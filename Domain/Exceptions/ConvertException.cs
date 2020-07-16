using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
    public class ConvertException : Exception
    {
        public ConvertException(string message) : base(message)
        {
            
        }
    }
}
