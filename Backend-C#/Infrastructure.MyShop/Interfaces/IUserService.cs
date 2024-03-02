using Infrastructure.MyShop.Models.DTO.AccountDTO;
using Infrastructure.MyShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MyShop.Interfaces
{
    public interface IUserService
    {
        Task<ServiceResponse> GetAllUsersAsync();
        Task<ServiceResponse> GetUserByIdAsync(long id);
        Task<ServiceResponse> RegisterUserAsync(UserCreateDTO model);
        Task<ServiceResponse> LoginUserAsync(LoginDTO model);
        Task<ServiceResponse> GoogleExternalLogin(ExternalLoginDTO model);
        Task<ServiceResponse> ConfirmEmailAsync(string userId, string token);
        Task<ServiceResponse> EditUserAsync(UserEditDTO model);
        Task<ServiceResponse> DeleteUserByIdAsync(long id);
    }
}
