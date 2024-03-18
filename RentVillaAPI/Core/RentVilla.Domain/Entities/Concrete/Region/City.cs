using RentVilla.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Domain.Entities.Concrete.Region
{
    public class City: BaseEntity
    {
        public State State { get; set; }
        public Guid StateId { get; set; }
        public string Name { get; set; }
    }
}
