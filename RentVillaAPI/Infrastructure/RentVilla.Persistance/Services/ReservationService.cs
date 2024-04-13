using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RentVilla.Application.Abstraction.Services;
using RentVilla.Application.DTOs.Reservation;
using RentVilla.Application.Repositories.ProductRepo;
using RentVilla.Application.Repositories.ReservationCartRepo;
using RentVilla.Application.Repositories.ReservationRepo;
using RentVilla.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Reservation reservation = new();
                reservation.Products = _productReadRepository.AppDbContext.Where(p => createReservation.ProductIds.Select(pId => Guid.Parse(pId)).Contains(p.Id)).ToList();
                reservation = _mapper.Map<Reservation>(createReservation);

                await _reservationWriteRepository.AddAsync(reservation);
                int savingStatus = await _reservationWriteRepository.SaveAsync();
                if (savingStatus == 200)
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
