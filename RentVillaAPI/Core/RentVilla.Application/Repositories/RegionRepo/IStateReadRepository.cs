using RentVilla.Application.DTOs.RegionDTOs;
using RentVilla.Application.Repositories;
using RentVilla.Application.ViewModels.Attribute;
using RentVilla.Domain.Entities.Concrete.Attribute;
using RentVilla.Domain.Entities.Concrete.Region;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Repositories.RegionRepo
{
    public interface IStateReadRepository : IReadRepository<State>
    {
        public ICollection<StateDTO> GetAllStates();
    }
}
