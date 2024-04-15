using RentVilla.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Domain.Entities.Concrete
{
    public class Menu: BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Endpoint> Endpoints { get; set; }
    }
}
