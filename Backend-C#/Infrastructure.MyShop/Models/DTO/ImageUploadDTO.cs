using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MyShop.Models.DTO
{
    public class ImageUploadDTO
    {
        public IFormFile Image { get; set; }
        public int ProductId { get; set; }
    }
}
