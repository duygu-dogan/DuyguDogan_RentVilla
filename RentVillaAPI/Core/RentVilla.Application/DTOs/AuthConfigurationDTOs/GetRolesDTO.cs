using RentVilla.Domain.Entities.Concrete.Identity;
using RentVilla.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.DTOs.AuthConfigurationDTOs
{
    public class GetRolesDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
