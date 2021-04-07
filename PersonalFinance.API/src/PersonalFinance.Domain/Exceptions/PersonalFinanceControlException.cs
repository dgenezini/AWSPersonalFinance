using System;

namespace PersonalFinance.Domain.Exceptions
{
    public class PersonalFinanceControlException : Exception
    {
        public PersonalFinanceControlException(string message) :
            base(message)
        {

        }

        public PersonalFinanceControlException(string message, Exception innerException) :
            base(message, innerException)
        {

        }
    }
}
