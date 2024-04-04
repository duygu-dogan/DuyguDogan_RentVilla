using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.Feature.Commands.AppUser.RefreshTokenLogin
{
    public class RefreshTokenLoginCommandRequest: IRequest<RefreshTokenLoginCommandResponse>
    {
        public string RefreshToken { get; set; }
    }
}
