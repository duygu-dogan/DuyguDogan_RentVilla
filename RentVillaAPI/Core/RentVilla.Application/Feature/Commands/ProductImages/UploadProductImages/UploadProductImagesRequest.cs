using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Feature.Commands.ProductImages.UploadProductImages
{
    public class UploadProductImagesRequest : IRequest<UploadProductImagesResponse>
    {
        public string ProductId { get; set; }
        public IFormFileCollection Files { get; set; }
    }
}
