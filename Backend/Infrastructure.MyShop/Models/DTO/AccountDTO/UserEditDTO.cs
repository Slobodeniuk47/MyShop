using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MyShop.Models.DTO.AccountDTO
{
    public class UserEditDTO
    {
        public long id { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public IFormFile ImageUpload { get; set; }
        public string phoneNumber { get; set; }
        public string? Role { get; set; }
    }
}
