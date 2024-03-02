using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.MyShop.Entities
{
    [Table("tbl_Categories")]
    public class CategoryEntity : BaseEntity<int>
    {
        //public DateTime DateLastEdit { get; set; }

        [StringLength(4000)]
        public string Description { get; set; }
        [StringLength(255)]
        public string Image { get; set; }

        [DisplayName("Category")]
        //Every category have a category
        public CategoryEntity Parent { get; set; }

        [ForeignKey(nameof(Parent))]
        public int? ParentId { get; set; }
        //Foreign keys:
        public ICollection<ProductEntity> Products { get; set; }
        public ICollection<CategoryEntity> Subcategories { get; set; } = new List<CategoryEntity>();
    }
}
