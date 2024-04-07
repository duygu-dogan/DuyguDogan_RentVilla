using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Feature.Commands.StateImages.UploadStateImages
{
    public class UploadStateImagesCommandRequest: IRequest<UploadStateImagesCommandResponse>
    {
        public string StateId { get; set; }
        public IFormFileCollection Files { get; set; }
    }
}
