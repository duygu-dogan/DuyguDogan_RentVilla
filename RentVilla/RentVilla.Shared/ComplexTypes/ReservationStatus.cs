using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Shared.ComplexTypes
{
    public enum ReservationStatus
    {
        Received = 0,
        Checking = 1,
        InUse = 2,
        Available = 3
    }
}
