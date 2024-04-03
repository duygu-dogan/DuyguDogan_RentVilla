using RentVilla.Domain.Entities.Concrete.Region;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.DTOs.RegionDTOs
{
    public class StateDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CountryId { get; set; }
    }
}
