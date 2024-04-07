using RentVilla.Domain.Entities.Concrete.Region;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RentVilla.Application.DTOs.RegionDTOs
{
    public class StateDTO
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("countryId")]
        public string CountryId { get; set; }
        [JsonPropertyName("images")]
        public List<string> Images { get; set; }
    }
}
