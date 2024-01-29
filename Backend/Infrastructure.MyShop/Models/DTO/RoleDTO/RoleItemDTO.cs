using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MyShop.Models.DTO.RoleDTO
{
    public class RoleItemDTO
    {
        public long Id { get; set; }
        public string RoleName { get; set; }
        public string? ConcurrencyStamp { get; set; }
    }
}
