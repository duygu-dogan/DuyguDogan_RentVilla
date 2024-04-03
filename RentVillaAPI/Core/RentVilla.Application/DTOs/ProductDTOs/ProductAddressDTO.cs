using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.DTOs.ProductDTOs
{
    public class ProductAddressDTO
    {
        public string Id { get; set; }
        public string CountryName { get; set; }
        public string CountryId { get; set; }
        public string StateName { get; set; }
        public string StateId { get; set; }
        public string CityName { get; set; }
        public string CityId { get; set; }
        public string DistrictName { get; set; }
        public string DistrictId { get; set; }
    }
}
