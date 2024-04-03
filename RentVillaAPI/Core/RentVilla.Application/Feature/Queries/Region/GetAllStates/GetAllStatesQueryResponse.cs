using RentVilla.Application.DTOs.RegionDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Feature.Queries.Region.GetAllStates
{
    public class GetAllStatesQueryResponse
    {
        public List<StateDTO> StateDTOs { get; set; }
    }
}
