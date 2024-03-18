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
        public Guid CountryId { get; set; }
        public State State { get; set; }
        public Guid StateId { get; set; }
        public City City { get; set; }
        public Guid CityId { get; set; }
        public District District { get; set; }
        public Guid DistrictId { get; set; }
    }
}
