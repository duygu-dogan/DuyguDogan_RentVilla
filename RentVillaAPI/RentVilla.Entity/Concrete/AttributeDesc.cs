using RentVilla.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Entity.Concrete
{
    public class AttributeDesc : IMainEntity
    {
        public Guid Id { get; set ; }
        public Guid AttributeTypeId { get; set; }
        public AttributeType AttributeType { get; set; }
        public string Name { get; set; }
    }
}
