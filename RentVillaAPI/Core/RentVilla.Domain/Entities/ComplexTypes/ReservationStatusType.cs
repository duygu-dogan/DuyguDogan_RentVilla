﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Domain.Entities.ComplexTypes
{
    public enum ReservationStatusType
    {
        Received = 0,
        Checking = 1,
        InUse = 2,
        Cleaning =3,
        Available = 4
    }
}
