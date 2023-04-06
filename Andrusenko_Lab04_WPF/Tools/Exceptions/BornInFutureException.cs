using System;

namespace Andrusenko_Lab04_WPF.Exceptions
{
    public class BornInFutureException : Exception
    {
        public BornInFutureException()
        {
        }
        public BornInFutureException(string message) : base(message)
        {
        }
    }
}
