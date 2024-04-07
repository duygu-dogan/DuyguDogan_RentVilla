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

namespace RentVilla.Application.Feature.Commands.StateImages.UploadStateImages
{
    public class UploadStateImagesCommandHandler : IRequestHandler<UploadStateImagesCommandRequest, UploadStateImagesCommandResponse>
    {
        private readonly IStorageService _storageService;
        private readonly IStateReadRepository _stateReadRepository;
        private readonly IStateImageFileWriteRepository _stateImageFileWriteRepository;
        readonly ILogger<StateImageFile> _logger;

        public UploadStateImagesCommandHandler(IStorageService storageService, IStateReadRepository stateReadRepository, IStateImageFileWriteRepository stateImageFileWriteRepository, ILogger<StateImageFile> logger)
        {
            _storageService = storageService;
            _stateReadRepository = stateReadRepository;
            _stateImageFileWriteRepository = stateImageFileWriteRepository;
            _logger = logger;
        }

        public async Task<UploadStateImagesCommandResponse> Handle(UploadStateImagesCommandRequest request, CancellationToken cancellationToken)
        {
          List<(string fileName, string containerName)> result = await _storageService.UploadAsync("state-images", request.Files);
            State state = await _stateReadRepository.GetByIdAsync(request.StateId);
            try
            {
                await _stateImageFileWriteRepository.AddRangeAsync(result.Select(r => new StateImageFile
                {
                    FileName = r.fileName,
                    Path = r.containerName,
                    Storage = _storageService.StorageName,
                    States = new List<State>() { state }
                }).ToList());
                await _stateImageFileWriteRepository.SaveAsync();
                return new();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new ImageFileUploadFailException();
            }

        }
    }
}
