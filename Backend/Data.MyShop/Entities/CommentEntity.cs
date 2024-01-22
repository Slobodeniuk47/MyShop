using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.MyShop.Entities.Identity;

namespace Data.MyShop.Entities
{
    [Table("tblComments")]
    public class CommentEntity : BaseEntity<int>
    {

        public string Title { get; set; }


        [Required(ErrorMessage = "The message field is required.")]
        public string Message { get; set; }


        [Range(0, 5, ErrorMessage = "The Stars field must be between 0 and 5.")]
        public int Stars { get; set; }
        public int Likes { get; set; }

        public int Dislikes { get; set; }

        //Foreign keys:

        //Every User have a company
        public UserEntity User { get; set; }

        [ForeignKey(nameof(User))]
        public long UserId { get; set; }


        //Every User have a company
        public ProductEntity Product { get; set; }

        [ForeignKey(nameof(Product))]
        public int? ProductId { get; set; }


        public virtual ICollection<CommentImageEntity>? CommentImages { get; set; } = null;
    }
}
