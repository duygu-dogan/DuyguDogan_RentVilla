using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace RentVilla.MVC.Models.Product
{
    public class ProductFilterVM
    {
        public ProductFilterVM()
        {
            ProductStateList = new List<SelectListItem>();
            ProductAttributeTypeList = new List<SelectListItem>();
        }

        [DisplayName("StateList")]
        [JsonPropertyName("ProductStateList")]
        public List<SelectListItem> ProductStateList { get; set; }
        [JsonPropertyName("SelectedState")]
        public string SelectedState { get; set; }

        [DisplayName("AttributeList")]
        [JsonPropertyName("ProductAttributeTypeList")]
        public List<SelectListItem> ProductAttributeTypeList { get; set; }
        [JsonPropertyName("SelectedAttribute")]
        public string SelectedAttribute { get; set; }
        [DisplayName("StartDate")]
        [JsonPropertyName("StartDate")]
        public DateTime StartDate { get; set; }
        [DisplayName("EndDate")]
        [JsonPropertyName("EndDate")]
        public DateTime EndDate { get; set; }
    }
}
