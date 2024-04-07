using MediatR;
using Microsoft.Extensions.Logging;
using RentVilla.Application.Exceptions;
using RentVilla.Application.Repositories.FileRepo;
using RentVilla.Domain.Entities.Concrete.Region;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Feature.Queries.StateImages.GetStateImages
{
    public class GetStateImagesQueryHandler : IRequestHandler<GetStateImagesQueryRequest, GetStateImagesQueryResponse>
    {
        private readonly IStateImageFileReadRepository _stateImageFileReadRepository;
        readonly ILogger<StateImageFile> _logger;

        public GetStateImagesQueryHandler(IStateImageFileReadRepository stateImageFileReadRepository, ILogger<StateImageFile> logger)
        {
            _stateImageFileReadRepository = stateImageFileReadRepository;
            _logger = logger;
        }

        public async Task<GetStateImagesQueryResponse> Handle(GetStateImagesQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                List<StateImageFile> imageFiles = _stateImageFileReadRepository.GetAll().ToList();
                return new() { imageFiles = imageFiles };
            }
            catch (Exception)
            {
                _logger.LogError("Error in GetStateImagesQueryHandler");
                throw new ImageFileNotFoundException();
            }
        }
    }
}
