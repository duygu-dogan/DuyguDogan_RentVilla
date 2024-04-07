using MediatR;
using Microsoft.EntityFrameworkCore;
using RentVilla.Application.DTOs.RegionDTOs;
using RentVilla.Application.Repositories.FileRepo;
using RentVilla.Application.Repositories.RegionRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Feature.Queries.Region.GetAllStates
{
    public class GetAllStatesQueryHandler : IRequestHandler<GetAllStatesQueryRequest, GetAllStatesQueryResponse>
    {
        private readonly IStateReadRepository _stateReadRepository;
        private readonly IStateImageFileReadRepository _stateImageFileReadRepository;

        public GetAllStatesQueryHandler(IStateReadRepository stateReadRepository, IStateImageFileReadRepository stateImageFileReadRepository = null)
        {
            _stateReadRepository = stateReadRepository;
            _stateImageFileReadRepository = stateImageFileReadRepository;
        }

        public async Task<GetAllStatesQueryResponse> Handle(GetAllStatesQueryRequest request, CancellationToken cancellationToken)
        {
            var states = _stateReadRepository.AppDbContext.Include(states => states.StateImageFiles).Select(state => new StateDTO
            {
                Id = state.Id.ToString(),
                Name = state.Name,
                CountryId = state.CountryId.ToString(),
                Images = state.StateImageFiles.ToList().Select(x => x.Path).ToList()
            }).ToList();
           
            if (states.Count > 0)
            {
                return new GetAllStatesQueryResponse
                {
                    StateDTOs = states.ToList()
                };
            }
            throw new Exception("States not found");
        }
    }
}
