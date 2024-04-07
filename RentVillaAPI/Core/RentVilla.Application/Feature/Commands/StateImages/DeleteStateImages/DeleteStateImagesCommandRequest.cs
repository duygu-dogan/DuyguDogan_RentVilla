using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Feature.Commands.StateImages.DeleteStateImages
{
    public class DeleteStateImagesCommandRequest: IRequest<DeleteStateImagesCommandResponse>
    {
        public string pathOrContainerName { get; set; }
        public string fileName { get; set; }
        public string StateId { get; set; }
    }
}
