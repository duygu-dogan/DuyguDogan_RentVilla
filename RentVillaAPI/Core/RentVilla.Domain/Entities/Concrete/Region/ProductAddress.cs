using RentVilla.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Domain.Entities.Concrete.Region
{
  public class ProductAddress: BaseEntity, IAdressEntity
    {
        public string ProductId { get; set; }
        public Product Product { get; set; }
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
