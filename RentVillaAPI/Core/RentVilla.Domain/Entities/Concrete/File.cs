using RentVilla.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Domain.Entities.Concrete
{
    public class File: BaseEntity
    {
        [NotMapped]
        public override DateTime UpdatedAt { get => base.UpdatedAt; set => base.UpdatedAt = value; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public string Storage { get; set; }
    }
}
