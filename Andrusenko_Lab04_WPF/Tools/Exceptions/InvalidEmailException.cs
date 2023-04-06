using System;

namespace Andrusenko_Lab04_WPF.Exceptions
{
    public class InvalidEmailException : Exception
    {
        public InvalidEmailException()
        {
        }
        public InvalidEmailException(string message) : base(message)
        {
        }
    }
}
