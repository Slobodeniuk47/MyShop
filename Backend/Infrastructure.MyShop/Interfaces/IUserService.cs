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
        Task<ServiceResponse> LoginUserAsync(LoginDTO model);
        Task<ServiceResponse> GetAllUsersAsync();
        Task<ServiceResponse> RegisterUserAsync(UserCreateDTO model);
        Task<ServiceResponse> ConfirmEmailAsync(String userId, string token);
        Task<ServiceResponse> EditUserAsync(UserEditDTO model);
        Task<ServiceResponse> GetUserByIdAsync(int id);
        Task<ServiceResponse> DeleteUserByIdAsync(int id);
    }
}
