using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Exceptions
{
    public class ImageFileNotFoundException : Exception
    {
        public ImageFileNotFoundException()
        {
        }

        public ImageFileNotFoundException(string message) : base("An error occured while uploading the file.")
        {
        }
    }
}
