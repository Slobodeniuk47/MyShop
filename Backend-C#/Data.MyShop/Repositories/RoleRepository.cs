using Data.MyShop.Entities;
using Data.MyShop.Entities.Identity;
using Data.MyShop.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.MyShop.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<RoleEntity> _roleManager;
        private protected readonly ApplicationDbContext _dbContext;
        public RoleRepository(ApplicationDbContext dbContext, RoleManager<RoleEntity> roleManager) 
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
        }
        public IQueryable<RoleEntity> GetAll()
        {
            return _dbContext.Set<RoleEntity>().AsNoTracking();
        }
        public IQueryable<RoleEntity> Roles => GetAll();
        public async Task<List<RoleEntity>> GetAllRolesAsync()
        {
            var result = await _roleManager.Roles.ToListAsync();
            return result;
        }
        public async Task<RoleEntity> GetRoleByIdAsync(string id)
        {
            var result = await _roleManager.FindByIdAsync(id);
            return result;
        }
        public async Task<RoleEntity> GetRoleByNameAsync(string roleName)
        {
            var result = await _roleManager.FindByNameAsync(roleName);
            return result;
        }
        public async Task<IdentityResult> CreateRoleAsync(RoleEntity role)
        {
            var result = await _roleManager.CreateAsync(role);
            return result;
        }
        public async Task<IdentityResult> UpdateRoleAsync(RoleEntity role)
        {
            var result = await _roleManager.UpdateAsync(role);
            return result;
        }
        public async Task<IdentityResult> DeleteRoleAsync(RoleEntity role)
        {
            var result = await _roleManager.DeleteAsync(role);
            return result;
        }
    }
}
