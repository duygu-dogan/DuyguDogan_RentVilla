using MediatR;

namespace RentVilla.Application.Feature.Commands.Reservations.CreateReservation
{
    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommandRequest, CreateReservationCommandResponse>
    {
        public Task<CreateReservationCommandResponse> Handle(CreateReservationCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
