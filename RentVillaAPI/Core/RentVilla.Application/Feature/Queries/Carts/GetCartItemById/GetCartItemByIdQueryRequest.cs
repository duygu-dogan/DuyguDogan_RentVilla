using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Feature.Queries.Carts.GetCartItemById
{
    public class GetCartItemByIdQueryRequest: IRequest<GetCartItemByIdQueryResponse>
    {
        public string CartItemId { get; set; }
    }
}
