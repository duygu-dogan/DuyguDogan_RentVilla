using RentVilla.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Domain.Entities.Concrete.Region
{
    public class District: BaseEntity
    {
        public string Name { get; set; }
        public City City { get; set; }
        public Guid CityId { get; set; }

    }
}
