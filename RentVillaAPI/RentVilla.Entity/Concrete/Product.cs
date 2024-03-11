using RentVilla.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Entity.Concrete
{
    public class Product : BaseEntity, IMainEntity
    {
        public Guid Id { get; set ; }
        public decimal Price { get; set; }
        public decimal Deposit { get; set; }
        public List<string> ImageUrl { get; set; }
        public string MapId { get; set; }
        public int ShortestRentPeriod { get; set; }
        public string Properties { get; set; }
        public string Rating { get; set; }
    }
}
