using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.MyShop.Entities
{
    [Table("tbl_Products")]
    public class ProductEntity : BaseEntity<int>
    {
        public string Description { get; set; }
        public double Price { get; set; }

        [DisplayName("Category")]
        //Every product have a category
        public CategoryEntity Category { get; set; }

        [ForeignKey(nameof(Category))]
        public int? CategoryId { get; set; }
        public virtual ICollection<ProductImageEntity> ProductImages { get; set; }
        public virtual ICollection<CommentEntity> Comments { get; set; }
    }
}
