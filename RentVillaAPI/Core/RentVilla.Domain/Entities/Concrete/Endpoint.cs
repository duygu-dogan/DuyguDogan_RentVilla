using RentVilla.Domain.Entities.Abstract;
using RentVilla.Domain.Entities.Concrete.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Domain.Entities.Concrete
{
    public class Endpoint: BaseEntity
    {
        public Endpoint()
        {
            Roles = new HashSet<AppRole>();
        }
        public Menu Menu { get; set; }
        public ICollection<AppRole> Roles { get; set; }
        public string ActionType { get; set; }
        public string HttpType { get; set; }
        public string Code { get; set; }
        public string Definition { get; set; }
    }
}
