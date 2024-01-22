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
    public class UserRepository : IUserRepository
    {

        private readonly UserManager<UserEntity> _userManager;

        public UserRepository(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }


        public async Task<bool> ValidatePasswordAsync(string email, string password)
        {
            var result = await _userManager.CheckPasswordAsync(await _userManager.FindByEmailAsync(email), password);
            return result;
        }
        public async Task<IdentityResult> RegisterUserAsync(UserEntity user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result;
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(UserEntity user)
        {
            var result = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return result;
        }

        public async Task<UserEntity> GetUserByIdAsync(string id)
        {
            var result = await _userManager.FindByIdAsync(id);
            return result;
        }

        public async Task<IdentityResult> ConfirmEmailAsync(UserEntity user, string token)
        {
            var result = await _userManager.ConfirmEmailAsync(user, token);
            return result;
        }

        public async Task<UserEntity> GetUserByEmailAsync(string email)
        {
            var result = await _userManager.FindByEmailAsync(email);
            return result;
        }

        public async Task<string> GeneratePasswordResetTokenAsync(UserEntity user)
        {
            var result = await _userManager.GeneratePasswordResetTokenAsync(user);
            return result;
        }

        async Task<IList<string>> IUserRepository.GetRolesAsync(UserEntity user)
        {
            var result = await _userManager.GetRolesAsync(user);
            return result;
        }

        public async Task<List<UserEntity>> GetAllUsersAsync()
        {
            var result = await _userManager.Users.ToListAsync();
            return result;
        }

        public async Task<UserEntity> UpdateUserAsync(UserEntity user)
        {
            await _userManager.UpdateAsync(user);
            return user;
        }

        public async Task<UserEntity> AddRoleAsync(UserEntity user, string role)
        {
            await _userManager.AddToRoleAsync(user, role);
            return user;
        }
        public async Task<UserEntity> RemoveRoleAsync(UserEntity user, IList<string> role)
        {
            await _userManager.RemoveFromRolesAsync(user, role);
            return user;
        }
        public async Task<UserEntity> DeleteUserAsync(UserEntity user)
        {
            await _userManager.DeleteAsync(user);
            return user;
        }
    }
}
