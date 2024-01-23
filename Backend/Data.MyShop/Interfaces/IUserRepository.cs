using Data.MyShop.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.MyShop.Interfaces
{
    public interface IUserRepository
    {
        Task<IdentityResult> RegisterUserAsync(UserEntity model, string password);
        Task<bool> ValidateLoginAsync(string email, string password);
        Task<UserEntity> GetUserByIdAsync(string id);
        Task<UserEntity> UpdateUserAsync(UserEntity model);
        Task<UserEntity> GetUserByEmailAsync(string email);
        Task<IList<string>> GetRolesAsync(UserEntity model);
        Task<IdentityResult> ConfirmEmailAsync(UserEntity model, string token);
        Task<string> GeneratePasswordResetTokenAsync(UserEntity model);
        Task<string> GenerateEmailConfirmationTokenAsync(UserEntity appUser);
        Task<List<UserEntity>> GetAllUsersAsync();
        Task<UserEntity> AddRoleAsync(UserEntity model, string role);
        Task<UserEntity> RemoveRoleAsync(UserEntity model, IList<string> role);
        Task<UserEntity> DeleteUserAsync(UserEntity user);

    }
}
