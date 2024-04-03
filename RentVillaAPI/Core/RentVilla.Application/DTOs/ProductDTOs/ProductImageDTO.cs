using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.DTOs.ProductDTOs
{
    public class ProductImageDTO
    {
        public string FileName { get; set; }
        public string Path { get; set; }
        public List<string> ProductId { get; set; }
    }
}
