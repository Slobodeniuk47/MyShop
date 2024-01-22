using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.MyShop.Entities
{
    [Table("tblCommentImages")]
    public class CommentImageEntity : BaseEntity<int>
    {
        //Foreign keys:

        //Every User have a company
        public CommentEntity Comment { get; set; }

        [ForeignKey(nameof(Comment))]
        public int? CommentId { get; set; }
    }
}
