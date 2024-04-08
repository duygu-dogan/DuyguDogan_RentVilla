using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Feature.Queries.Carts.GetCartItems
{
    public class GetCartItemQueryRequest: IRequest<GetCartItemQueryResponse>
    {
    }
}
