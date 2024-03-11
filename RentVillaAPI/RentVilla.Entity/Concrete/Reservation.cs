using RentVilla.Entity.Abstract;
using RentVilla.Entity.Concrete.Identity;
using RentVilla.Shared.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Entity.Concrete
{
    public class Reservation : IMainEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal AddServiceCost { get; set; }
        public decimal TotalCost { get; set; }
        public string ConversationId { get; set; }
        public string PaymentId { get; set; }
        public ReservationStatus Status { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public AdditionalServices AdditionalServices { get; set; }

    }
}
