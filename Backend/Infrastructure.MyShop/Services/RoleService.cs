using Data.MyShop.Entities.Identity;
using Data.MyShop.Interfaces;
using Data.MyShop.Repositories;
using Infrastructure.MyShop.Interfaces;
using Infrastructure.MyShop.Models.DTO.AccountDTO;
using Infrastructure.MyShop.Models.DTO.RoleDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MyShop.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository) 
        { 
            _roleRepository = roleRepository;
        }
        public async Task<ServiceResponse> GetAllRolesAsync()
        {
            var result = await _roleRepository.Roles.Select(role => new RoleItemDTO
            {
                Id = role.Id,
                RoleName = role.Name,
                ConcurrencyStamp = role.ConcurrencyStamp
            }).ToListAsync();
            return new ServiceResponse()
            {
                IsSuccess = true,
                Message = "Get all roles",
                Payload = result
            };
        }
        public async Task<ServiceResponse> GetRoleByIdAsync(long id)
        {
            var result = await _roleRepository.Roles.Where(role => role.Id == id).Select(role => new RoleItemDTO
            {
                Id = role.Id,
                RoleName = role.Name,
                ConcurrencyStamp = role.ConcurrencyStamp
            }).ToListAsync();
            return new ServiceResponse()
            {
                IsSuccess = true,
                Message = "Get role",
                Payload = result
            };
        }
        public async Task<ServiceResponse> CreateRoleAsync(RoleCreateDTO model)
        {
            var role = new RoleEntity { Name = model.RoleName };
            var result = await _roleRepository.CreateRoleAsync(role);
            return new ServiceResponse()
            {
                IsSuccess = true,
                Message = "The role has been created",
                Payload = result
            };
        }
        public async Task<ServiceResponse> EditRoleAsync(RoleEditDTO model)
        {  
            var role = new RoleEntity { Id = model.Id, Name = model.RoleName, ConcurrencyStamp = model.ConcurrencyStamp };
            var result = await _roleRepository.UpdateRoleAsync(role);
            return new ServiceResponse()
            {
                IsSuccess = true,
                Message = "The role has been updated",
                Payload = result
            };
        }
        public async Task<ServiceResponse> DeleteRoleByIdAsync(long id)
        {
            var role = await _roleRepository.GetRoleByIdAsync(id.ToString());
            await _roleRepository.DeleteRoleAsync(role);
            return new ServiceResponse()
            {
                IsSuccess = true,
                Message = "The role has been deleted",
                Payload = role
            };
        }
    }
}
