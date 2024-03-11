using RentVilla.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Entity.Concrete
{
    public class ItemAttribute : IMainEntity
    {
        public Guid Id { get; set ; }
        public Guid AttTypeId { get; set; }
        public AttributeType AttributeType { get; set; }
        public Guid AttId { get; set; }
        public AttributeDesc AttributeDesc { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
