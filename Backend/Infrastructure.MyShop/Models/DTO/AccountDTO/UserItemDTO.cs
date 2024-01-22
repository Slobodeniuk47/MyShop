using Infrastructure.MyShop.Models.DTO.CategoryDTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MyShop.Models.DTO.AccountDTO
{
    public class UserItemDTO
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Image { get; set; }
        public string phoneNumber { get; set; }
        public List<PermissionItemDTO> Permissions { get; set; }
    }
}
