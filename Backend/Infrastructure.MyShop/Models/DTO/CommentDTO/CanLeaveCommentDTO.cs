using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MyShop.Models.DTO.CommentDTO
{
    public class CanLeaveCommentDTO
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
    }
}
