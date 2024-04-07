using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Exceptions
{
    public class ImageFileDeleteFailException : Exception
    {
        public ImageFileDeleteFailException()
        {
        }

        public ImageFileDeleteFailException(string message) : base("An error occured while uploading the file.")
        {
        }
    }
}
