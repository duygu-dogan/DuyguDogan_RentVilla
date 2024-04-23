using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Feature.Queries.Products.GetByRegionProducts
{
    public class GetByRegionProductsQueryRequest:IRequest<GetByRegionProductsQueryResponse>
    {
        public string RegionId { get; set; }
    }
}
