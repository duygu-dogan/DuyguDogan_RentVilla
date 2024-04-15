using RentVilla.Domain.Entities.Concrete.Region;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Domain.Entities.Abstract
{
    public interface IAdressEntity
    {
        public Country Country { get; set; }
        public string CountryId { get; set; }
        public State State { get; set; }
        public string StateId { get; set; }
        public City City { get; set; }
        public string CityId { get; set; }
        public District District { get; set; }
        public string DistrictId { get; set; }
    }
}
