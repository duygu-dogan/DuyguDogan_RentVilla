using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.ViewModels.Attribute
{
    public class AttributeReadVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string TypeId { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
