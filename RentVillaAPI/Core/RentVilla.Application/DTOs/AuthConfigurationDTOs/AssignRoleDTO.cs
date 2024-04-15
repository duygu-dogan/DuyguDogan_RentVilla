using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.DTOs.AuthConfigurationDTOs
{
    public class AssignRoleDTO
    {
        public string[] RoleIds { get; set; }
        public string Code { get; set; }
        public Type? Type { get; set; }
        public string Menu { get; set; }
    }
}
