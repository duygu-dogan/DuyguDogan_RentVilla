using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RentVilla.MVC.Models.Address
{
    public class UserAddressVM
    {
        [DisplayName("State")]
        [Required(ErrorMessage = "Please enter your state.")]
        [JsonPropertyName("stateId")]
        public string? SelectedStateId { get; set; }

        [DisplayName("City")]
        [Required(ErrorMessage = "Please enter your city.")]
        [JsonPropertyName("cityId")]
        public string? SelectedCityId { get; set; }

        [DisplayName("District")]
        [Required(ErrorMessage = "Please enter your district.")]
        [JsonPropertyName("districtId")]
        public string? SelectedDistrictId { get; set; } 

        public List<SelectListItem> States { get; set; }
        public List<SelectListItem> Cities { get; set; }
        public List<SelectListItem> Districts { get; set; }
    }
}
