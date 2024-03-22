using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.ViewModels.Attribute
{
    public class AttributeCreateVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
