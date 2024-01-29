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

        public async Task<IdentityResult> AddLoginAsync(UserEntity user, UserLoginInfo loginInfo)
        {
            var result = await _userManager.AddLoginAsync(user, loginInfo);
            return result;
        }
        public async Task<bool> ValidateLoginAsync(string email, string password)
        {
            var result = await _userManager.CheckPasswordAsync(await _userManager.FindByEmailAsync(email), password);
            return result;
        }
        public async Task<IdentityResult> CreateUserAsync(UserEntity user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result;
        }
        public async Task<IdentityResult> CreateUserAsync(UserEntity user)
        {
            var result = await _userManager.CreateAsync(user);
            return result;
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(UserEntity user)
        {
            var result = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return result;
        }
        public async Task<UserEntity> GetUserByLoginAsync(string loginProvider, string providerKey)
        {
            var result = await _userManager.FindByLoginAsync(loginProvider, providerKey);
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

        public async Task<IList<string>> GetRolesAsync(UserEntity user)
        {
            var result = await _userManager.GetRolesAsync(user);
            return result;
        }

        public async Task<List<UserEntity>> GetAllUsersAsync()
        {
            var result = await _userManager.Users.ToListAsync();
            return result;
        }

        public async Task<IdentityResult> UpdateUserAsync(UserEntity user)
        {
            var result = await _userManager.UpdateAsync(user);
            return result;
        }

        public async Task<IdentityResult> AddRoleAsync(UserEntity user, string role)
        {
            var result = await _userManager.AddToRoleAsync(user, role);
            return result;
        }
        public async Task<IdentityResult> RemoveRolesAsync(UserEntity user, IList<string> role)
        {
            var result = await _userManager.RemoveFromRolesAsync(user, role);
            return result;
        }
        public async Task<IdentityResult> DeleteUserAsync(UserEntity user)
        {
            var result = await _userManager.DeleteAsync(user);
            return result;
        }
    }
}
