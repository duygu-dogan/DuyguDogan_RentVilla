using RentVilla.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Entity.Concrete
{
    public class AttributeType: IMainEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
