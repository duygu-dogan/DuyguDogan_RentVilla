using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Domain.Entities.Concrete.Region
{
    public class StateImageFile: File
    {
        public ICollection<State> States { get; set; }
    }
}
