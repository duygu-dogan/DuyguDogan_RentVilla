using RentVilla.Domain.Entities.Concrete.Identity;
using RentVilla.Domain.Entities.Concrete.Region;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.DTOs.AppUserDTOs
{
    public class AppUserAddressDTO
    {
        public string StateId { get; set; }
        public string CityId { get; set; }
        public string DistrictId { get; set; }
    }
}
