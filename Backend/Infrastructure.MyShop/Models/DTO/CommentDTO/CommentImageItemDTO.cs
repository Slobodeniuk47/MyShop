using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MyShop.Models.DTO.CommentDTO
{
    public class CommentImageItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? CommentId { get; set; }
        public string CommentName { get; set; }
    }
}
