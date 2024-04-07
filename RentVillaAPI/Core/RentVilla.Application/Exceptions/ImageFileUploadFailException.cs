using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Exceptions
{
    public class ImageFileUploadFailException : Exception
    {
        public ImageFileUploadFailException()
        {
        }

        public ImageFileUploadFailException(string message) : base("An error occured while uploading the file.")
        {
        }
    }
}
