using Microsoft.AspNetCore.Identity;

namespace Data.MyShop.Entities.Identity
{
    public class PermissionsEntity : IdentityUserRole<long>
    {
        public virtual UserEntity User { get; set; }
        public virtual RoleEntity Role { get; set; }
    }
}
