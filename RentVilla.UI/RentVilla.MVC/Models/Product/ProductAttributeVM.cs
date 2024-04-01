using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RentVilla.MVC.Models.Product
{
    public class ProductAttributeVM
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("attribute")]
        public string Attribute { get; set; }
        [JsonPropertyName("attributeType")]
        public string AttributeType { get; set; }
    }
}
