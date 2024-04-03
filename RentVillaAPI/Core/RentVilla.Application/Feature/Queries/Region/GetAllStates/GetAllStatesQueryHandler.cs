using MediatR;
using RentVilla.Application.DTOs.RegionDTOs;
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

        public GetAllStatesQueryHandler(IStateReadRepository stateReadRepository)
        {
            _stateReadRepository = stateReadRepository;
        }

        public async Task<GetAllStatesQueryResponse> Handle(GetAllStatesQueryRequest request, CancellationToken cancellationToken)
        {
            var states = _stateReadRepository.GetAllStates();
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
