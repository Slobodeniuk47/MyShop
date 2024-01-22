using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.MyShop.Entities
{
    [Table("tbl_ProductImages")]
    public class ProductImageEntity : BaseEntity<int>
    {
        public ProductEntity Product { get; set; }

        [ForeignKey(nameof(Product))]
        public int? ProductId { get; set; }

    }
}
