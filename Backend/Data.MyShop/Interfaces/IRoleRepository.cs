using Data.MyShop.Entities;
using Data.MyShop.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.MyShop.Interfaces
{
    public interface IRoleRepository
    {
        IQueryable<RoleEntity> GetAll();
        IQueryable<RoleEntity> Roles { get; }
        Task<List<RoleEntity>> GetAllRolesAsync();
        Task<RoleEntity> GetRoleByIdAsync(string id);
        Task<RoleEntity> GetRoleByNameAsync(string roleName);
        Task<IdentityResult> CreateRoleAsync(RoleEntity role);
        Task<IdentityResult> UpdateRoleAsync(RoleEntity role);
        Task<IdentityResult> DeleteRoleAsync(RoleEntity role);
    }
}
