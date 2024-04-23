using System.Text.Json.Serialization;

namespace RentVilla.MVC.Models.Reservation
{
    public class CreateReservationVM
    {
        [JsonPropertyName("AppUserId")]
        public string AppUserId { get; set; }
        [JsonPropertyName("ProductId")]
        public string ProductId { get; set; }
        [JsonPropertyName("ProductName")]
        public string ProductName { get; set; }
        [JsonPropertyName("StartDate")]
        public DateTime StartDate { get; set; }
        [JsonPropertyName("EndDate")]
        public DateTime EndDate { get; set; }
        [JsonPropertyName("AdultNumber")]
        public int AdultNumber { get; set; }
        [JsonPropertyName("ChildrenNumber")]
        public int ChildrenNumber { get; set; }
        [JsonPropertyName("Note")]
        public string Note { get; set; }
        [JsonPropertyName("ProductPrice")]
        public decimal ProductPrice { get; set; }
        [JsonPropertyName("TotalCost")]
        public decimal TotalCost { get; set; }
        [JsonPropertyName("IsPaid")]
        public bool IsPaid { get; set; }
        [JsonPropertyName("ConversationId")]
        public string ConversationId { get; set; }
        [JsonPropertyName("PaymentId")]
        public string PaymentId { get; set; }
        [JsonPropertyName("PaymentType")]
        public int PaymentType { get; set; } = 0;
        [JsonPropertyName("PaymentMethod")]
        public string PaymentMethod { get; set; }
        [JsonPropertyName("PaymentData")]
        public CreatePaymentVM? PaymentData { get; set; }
    }
}
