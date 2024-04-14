using AutoMapper;
using Microsoft.Extensions.Logging;
using RentVilla.Application.Abstraction.Services;
using RentVilla.Application.DTOs.ReservationDTOs;
using RentVilla.Application.Repositories.ProductRepo;
using RentVilla.Application.Repositories.ReservationCartRepo;
using RentVilla.Application.Repositories.ReservationRepo;
using RentVilla.Domain.Entities.ComplexTypes;
using RentVilla.Domain.Entities.Concrete;

namespace RentVilla.Persistence.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationWriteRepository _reservationWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IResCartWriteRepository _resCartWriteRepository;
        private readonly IResCartReadRepository _resCartReadRepository;
        readonly IMapper _mapper;
        readonly ILogger<ReservationService> _logger;

        public ReservationService(IReservationWriteRepository reservationWriteRepository, IProductReadRepository productReadRepository, IMapper mapper, IResCartWriteRepository resCartWriteRepository, IResCartReadRepository resCartReadRepository, ILogger<ReservationService> logger)
        {
            _reservationWriteRepository = reservationWriteRepository;
            _productReadRepository = productReadRepository;
            _mapper = mapper;
            _resCartWriteRepository = resCartWriteRepository;
            _resCartReadRepository = resCartReadRepository;
            _logger = logger;
        }

        public async Task CreateReservationAsync(CreateReservationDTO createReservation)
        {
            try
            {
                Reservation reservation = new()
                {
                    AppUserId = createReservation.AppUserId,
                    AdultNumber = createReservation.AdultNumber,
                    ChildrenNumber = createReservation.ChildrenNumber,
                    EndDate = createReservation.EndDate.ToUniversalTime(),
                    Note = createReservation.Note,
                    ProductId = Guid.Parse(createReservation.ProductId),
                    ConversationId = createReservation.AppUserId,
                    PaymentId = createReservation.PaymentId,
                    PaymentMethod = createReservation.PaymentMethod,
                    PaymentType = (PaymentType)Enum.Parse(typeof(PaymentType), createReservation.PaymentType.ToString()),
                    ProductPrice = createReservation.ProductPrice,
                    StartDate = createReservation.StartDate.ToUniversalTime(),
                    TotalCost = createReservation.TotalCost
                };

                await _reservationWriteRepository.AddAsync(reservation);
                int savingStatus = await _reservationWriteRepository.SaveAsync();
                if (savingStatus == 1)
                {
                    var userCart = await _resCartReadRepository.GetSingleAsync(rc => rc.UserId == createReservation.AppUserId);
                    await _resCartWriteRepository.DeleteAsync(userCart.Id.ToString());
                }
                else
                {
                    _logger.LogError("Error while saving reservation");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating reservation");
                throw;
            }
        }
    }
}
