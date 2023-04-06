using System;

namespace Andrusenko_Lab04_WPF.Exceptions
{
    public class TooOldException : Exception
    {
        public TooOldException()
        {
        }
        public TooOldException(string message) : base(message)
        {
        }
    }
}
