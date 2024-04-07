using MediatR;
using Microsoft.Extensions.Logging;
using RentVilla.Application.Abstraction.Storage;
using RentVilla.Application.Exceptions;
using RentVilla.Application.Repositories.FileRepo;
using RentVilla.Application.Repositories.RegionRepo;
using RentVilla.Domain.Entities.Concrete.Region;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Feature.Commands.StateImages.DeleteStateImages
{
    public class DeleteStateImagesCommandHandler : IRequestHandler<DeleteStateImagesCommandRequest, DeleteStateImagesCommandResponse>
    {
        private readonly IStorageService _storageService;
        private readonly IStateImageFileReadRepository _stateImageFileReadRepository;
        private readonly IStateImageFileWriteRepository _stateImageFileWriteRepository;
        readonly ILogger<StateImageFile> _logger;

        public DeleteStateImagesCommandHandler(IStorageService storageService, IStateImageFileReadRepository stateImageFileReadRepository, IStateImageFileWriteRepository stateImageFileWriteRepository, ILogger<StateImageFile> logger)
        {
            _storageService = storageService;
            _stateImageFileReadRepository = stateImageFileReadRepository;
            _stateImageFileWriteRepository = stateImageFileWriteRepository;
            _logger = logger;
        }

        public async Task<DeleteStateImagesCommandResponse> Handle(DeleteStateImagesCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                await _storageService.DeleteAsync(request.pathOrContainerName, request.fileName);
                StateImageFile file = await _stateImageFileReadRepository.GetSingleAsync(s => s.FileName == request.fileName && s.Path == request.pathOrContainerName);
                _stateImageFileWriteRepository.Delete(file);
                return new DeleteStateImagesCommandResponse();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DeleteStateImagesCommandHandler");
                throw new ImageFileDeleteFailException();
            }

        }
    }
}
