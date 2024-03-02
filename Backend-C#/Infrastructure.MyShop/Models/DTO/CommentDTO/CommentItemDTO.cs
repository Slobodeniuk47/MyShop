using Infrastructure.MyShop.Models.DTO.AccountDTO;
using Infrastructure.MyShop.Models.DTO.ProductDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MyShop.Models.DTO.CommentDTO
{
    public class CommentItemDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public int Stars { get; set; }
        public string DateCreated { get; set; }
        public string DateUpdated { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public long? UserId { get; set; }
        public string UserName { get; set; }
        public int? ProductId { get; set; }
        public UserItemDTO User { get; set; }
        public List<CommentImageItemDTO> Images { get; set; }
    }
}
