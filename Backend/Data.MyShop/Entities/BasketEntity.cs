using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.MyShop.Entities
{
    [Table("tbl_Basket")]
    public class BasketEntity
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }

        public short Count { get; set; }
        public DateTime DateCreate { get; set; } = DateTime.UtcNow;
    }
}
