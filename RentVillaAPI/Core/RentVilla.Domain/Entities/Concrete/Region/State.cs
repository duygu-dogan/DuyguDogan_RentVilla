using RentVilla.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Domain.Entities.Concrete.Region
{
    public class State: BaseEntity
    {
        public string Name { get; set; }
        public Country Country { get; set; }
        public Guid CountryId { get; set; }
    }
}
