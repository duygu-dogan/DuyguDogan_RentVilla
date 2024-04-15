using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using RentVilla.Application.Abstraction.Services;
using RentVilla.Application.DTOs.ReservationDTOs;
using RentVilla.Application.Repositories.ProductRepo;
using RentVilla.Application.Repositories.ReservationCartRepo;
using RentVilla.Application.Repositories.ReservationRepo;
using RentVilla.Domain.Entities.ComplexTypes;
using RentVilla.Domain.Entities.Concrete;
using RentVilla.Domain.Entities.Concrete.Identity;

namespace RentVilla.Persistence.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationWriteRepository _reservationWriteRepository;
        private readonly IReservationReadRepository _reservationReadRepository;
        private readonly IResCartWriteRepository _resCartWriteRepository;
        private readonly IResCartReadRepository _resCartReadRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly UserManager<AppUser> _userManager;
        readonly IMapper _mapper;
        readonly ILogger<ReservationService> _logger;

        public ReservationService(IReservationWriteRepository reservationWriteRepository, IMapper mapper, IResCartWriteRepository resCartWriteRepository, IResCartReadRepository resCartReadRepository, ILogger<ReservationService> logger, IReservationReadRepository reservationReadRepository, UserManager<AppUser> userManager, IProductReadRepository productReadRepository)
        {
            _reservationWriteRepository = reservationWriteRepository;
            _mapper = mapper;
            _resCartWriteRepository = resCartWriteRepository;
            _resCartReadRepository = resCartReadRepository;
            _logger = logger;
            _reservationReadRepository = reservationReadRepository;
            _userManager = userManager;
            _productReadRepository = productReadRepository;
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
                    ProductId = createReservation.ProductId,
                    ConversationId = createReservation.AppUserId,
                    PaymentId = createReservation.PaymentId,
                    PaymentMethod = createReservation.PaymentMethod,
                    PaymentType = (PaymentType)Enum.Parse(typeof(PaymentType), createReservation.PaymentType.ToString()),
                    ProductPrice = createReservation.ProductPrice,
                    StartDate = createReservation.StartDate.ToUniversalTime(),
                    TotalCost = createReservation.TotalCost,
                    Status = ReservationStatusType.Open
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


        public Task<GetReservationDTO> GetReservationByIdAsync(string reservationId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<GetReservationDTO>> GetActiveReservations()

        {
            List<Reservation> reservations = _reservationReadRepository.AppDbContext.Where(r => (int)r.Status == 0).ToList();
            List<GetReservationDTO> getReservationDTOs = _mapper.Map<List<GetReservationDTO>>(reservations);
            foreach (var reservation in getReservationDTOs)
            {
                AppUser user = await _userManager.FindByIdAsync(reservation.AppUserId);
                reservation.UserName = user.UserName;
                Product product = await _productReadRepository.GetByIdAsync(reservation.ProductId);
                reservation.ProductName = product.Name;
                reservation.ReservationStatus = reservations[0].Status.ToString();
                reservation.IsPaid = true;
            }
            return getReservationDTOs;
        }

        public Task<List<GetReservationDTO>> GetReservationsByProductIdAsync(string productId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<GetReservationDTO>> GetPassiveReservations()
        {
            List<Reservation> reservations = _reservationReadRepository.AppDbContext.Where(r => (int)r.Status == 1 || (int)r.Status == 2).ToList();
            List<GetReservationDTO> getReservationDTOs = _mapper.Map<List<GetReservationDTO>>(reservations);
            foreach (var reservation in getReservationDTOs)
            {
                AppUser user = await _userManager.FindByIdAsync(reservation.AppUserId);
                reservation.UserName = user.UserName;
                Product product = await _productReadRepository.GetByIdAsync(reservation.ProductId);
                reservation.ProductName = product.Name;
                reservation.ReservationStatus = reservations.Where(r => r.Id.ToString() == reservation.Id).FirstOrDefault().Status.ToString();
                reservation.IsPaid = true;
            }
            return getReservationDTOs;
        }
    }
}
