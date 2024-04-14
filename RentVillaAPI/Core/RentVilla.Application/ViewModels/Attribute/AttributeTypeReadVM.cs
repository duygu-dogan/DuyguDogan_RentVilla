using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.ViewModels.Attribute
{
    public class AttributeTypeReadVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<AttributeReadVM> Attributes { get; set; }
    }
}
