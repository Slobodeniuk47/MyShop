using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.MyShop.Entities.Identity
{
    public class RoleEntity : IdentityRole<long>
    {
        public virtual ICollection<PermissionsEntity> Permissions { get; set; }
    }
}
