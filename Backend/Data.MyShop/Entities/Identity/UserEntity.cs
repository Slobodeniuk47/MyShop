using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.MyShop.Entities.Identity
{
    [Table("tbl_Users")]
    public class UserEntity : IdentityUser<long>
    {
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string LastName { get; set; }
        [StringLength(200)]
        public string Image { get; set; }
        public virtual ICollection<PermissionsEntity> Permissions { get; set; }
        public virtual ICollection<CommentEntity> Comments { get; set; }
    }
}
