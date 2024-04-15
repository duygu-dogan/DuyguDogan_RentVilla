using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentVilla.Application.DTOs.UserDTOs
{
    public class AssignRoleToUserDTO
    {
        public string UserId { get; set; }
        public List<string> RoleIds { get; set; }
    }
}
