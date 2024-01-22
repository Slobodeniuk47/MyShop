using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MyShop.Models.DTO.CommentDTO
{
    public class CommentCreateDTO
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public int Stars { get; set; }
        public int UserId { get; set; }
        public int? ProductId { get; set; }
        public List<IFormFile>? Images { get; set; }
    }
}
