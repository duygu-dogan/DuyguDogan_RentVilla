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
            SelectedStates = new List<string>();
            SelectedAttributes = new List<string>();
        }

        [DisplayName("StateList")]
        public List<SelectListItem> ProductStateList { get; set; }

        [JsonPropertyName("SelectedStates")]
        public List<string> SelectedStates { get; set; }

        [DisplayName("AttributeList")]
        public List<SelectListItem> ProductAttributeTypeList { get; set; }

        [JsonPropertyName("SelectedAttributes")]
        public List<string> SelectedAttributes { get; set; }

        [DisplayName("StartDate")]
        [JsonPropertyName("StartDate")]
        public DateTime StartDate { get; set; }
        [DisplayName("EndDate")]
        [JsonPropertyName("EndDate")]
        public DateTime EndDate { get; set; }
    }
}
