using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Feature.Commands.ProductImages.DeleteProductImages
{
    public class DeleteProductImagesHandler : IRequestHandler<DeleteProductImagesRequest, DeleteProductImagesResponse>
    {
        public Task<DeleteProductImagesResponse> Handle(DeleteProductImagesRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
