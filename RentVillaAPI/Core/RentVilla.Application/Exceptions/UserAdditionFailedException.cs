using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Exceptions
{
    public class UserAdditionFailedException : Exception
    {
        public UserAdditionFailedException(): base("While adding user, an error occurred.")
        {
        }

        public UserAdditionFailedException(string message) : base(message)
        {
        }

        public UserAdditionFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
