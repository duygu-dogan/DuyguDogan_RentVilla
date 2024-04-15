using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RentVilla.Application.Abstraction.Iyzico;
using RentVilla.Application.Abstraction.Services;
using RentVilla.Application.DTOs.ReservationDTOs;
using RentVilla.Application.Repositories.ReservationCartRepo;

namespace RentVilla.Iyzico.IyzicoServices
{
    public class PaymentService : IPaymentService
    {
        private readonly IResCartReadRepository _resCartReadRepository;
        private readonly IResCartWriteRepository _resCartWriteRepository;
        private readonly IReservationService _reservationService;
        readonly IConfiguration _configuration;
        readonly ILogger<PaymentService> _logger;

        public PaymentService(IResCartReadRepository resCartReadRepository, IConfiguration configuration, IReservationService reservationService, IResCartWriteRepository resCartWriteRepository, ILogger<PaymentService> logger)
        {
            _resCartReadRepository = resCartReadRepository;
            _configuration = configuration;
            _reservationService = reservationService;
            _resCartWriteRepository = resCartWriteRepository;
            _logger = logger;
        }

        public async Task CompletePaymentAsync(CreateReservationDTO reservationDTO)
        {
            try
            {
                var reservationCart = await _resCartReadRepository.AppDbContext.Include(x => x.CartItems).FirstOrDefaultAsync(x => x.UserId == reservationDTO.AppUserId);

                var apiKey = _configuration["Iyzico:ApiKey"];
                var secretKey = _configuration["Iyzico:SecretKey"];
                var paymentData = reservationDTO.PaymentData;

                if (reservationDTO != null)
                {
                    Options options = new();
                    options.ApiKey = apiKey;
                    options.SecretKey = secretKey;
                    options.BaseUrl = _configuration["Iyzico:BaseUrl"];

                    CreatePaymentRequest request = new();
                    request.Locale = Locale.TR.ToString();
                    request.ConversationId = reservationDTO.AppUserId;
                    request.Price = reservationDTO.TotalCost.ToString().Replace(",", ".");
                    request.PaidPrice = reservationDTO.TotalCost.ToString().Replace(",", ".");
                    request.Currency = Currency.TRY.ToString();
                    request.Installment = 1;
                    request.BasketId = reservationCart.Id.ToString();
                    request.PaymentChannel = PaymentChannel.WEB.ToString();
                    request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

                    PaymentCard paymentCard = new();
                    paymentCard.CardHolderName = paymentData.CardName;
                    paymentCard.CardNumber = paymentData.CardNumber;
                    paymentCard.ExpireMonth = paymentData.ExpirationMonth;
                    paymentCard.ExpireYear = paymentData.ExpirationYear;
                    paymentCard.Cvc = paymentData.Cvc;
                    paymentCard.RegisterCard = 0;
                    request.PaymentCard = paymentCard;

                    Buyer buyer = new();
                    buyer.Id = reservationDTO.AppUserId;
                    buyer.Name = paymentData.FirstName;
                    buyer.Surname = paymentData.LastName;
                    buyer.GsmNumber = paymentData.PhoneNumber;
                    buyer.Email = paymentData.Email;
                    buyer.RegistrationAddress = paymentData.Address;
                    buyer.City = paymentData.City;
                    buyer.Country = "Turkey";
                    buyer.IdentityNumber = "74300864791";
                    request.Buyer = buyer;

                    Address billingAddress = new();
                    billingAddress.ContactName = paymentData.FirstName + " " + paymentData.LastName;
                    billingAddress.City = paymentData.City;
                    billingAddress.Country = "Turkey";
                    billingAddress.Description = paymentData.Address;
                    request.BillingAddress = billingAddress;

                    List<BasketItem> basketItems = new();
                    BasketItem item = new();
                    item.Id = reservationDTO.ProductId;
                    item.Name = reservationDTO.ProductName;
                    item.Category1 = "Accommodation";
                    item.ItemType = BasketItemType.VIRTUAL.ToString();
                    item.Price = reservationDTO.TotalCost.ToString().Replace(",", ".");
                    basketItems.Add(item);
                    request.BasketItems = basketItems;

                    Payment payment = Payment.Create(request, options);

                    if (payment.Status == "success")
                    {
                        reservationDTO.PaymentId = payment.PaymentId;
                        reservationDTO.PaymentMethod = "Iyzico";
                        reservationDTO.IsPaid = true;
                        reservationDTO.PaymentType = 0;
                        await _reservationService.CreateReservationAsync(reservationDTO);
                        await _resCartWriteRepository.DeleteAsync(reservationCart.Id.ToString());
                        await _resCartWriteRepository.SaveAsync();
                    }
                    else
                    {
                        _logger.LogError("Payment failed");
                        throw new Exception("Payment failed");
                    }
                }
                else
                {
                    _logger.LogError("Reservation data is null");
                    throw new Exception("Reservation data is null");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while completing payment", ex);
            }
        }
    }
}
