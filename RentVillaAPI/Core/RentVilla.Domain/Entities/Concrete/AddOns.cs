using RentVilla.Domain.Entities.Abstract;
using RentVilla.Domain.Entities.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Domain.Entities.Concrete
{
    public class AddOns: BaseEntity, IMainEntity
    {
        public AdditionalServiceType AdditionalServices { get; set; }
        public decimal Price { get; set; }
        public ICollection<Reservation> Reservation { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
