using AutoMapper;
using Data.MyShop.Constants;
using Data.MyShop.Entities.Identity;
using Data.MyShop.Interfaces;
using Infrastructure.MyShop.Helpers;
using Infrastructure.MyShop.Interfaces;
using Infrastructure.MyShop.Models.DTO.AccountDTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Abstractions;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MyShop.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private IConfiguration _configuration;
        //private EmailService _emailService;
        private IJwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;
        //private TokenValidationParameters _tokenValidationParameters;


        public UserService(IUserRepository userRepository, IJwtTokenService jwtTokenService, IConfiguration configuration, IMapper mapper)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _jwtTokenService = jwtTokenService;
            _mapper = mapper;
        }

        public async Task<ServiceResponse> RegisterUserAsync(UserCreateDTO model)
        {
            
            var img = await ImageHelper.SaveImageAsync(model.Image, DirectoriesInProject.UserImages);
            var user = _mapper.Map<UserEntity>(model);
            //var user = new UserEntity()
            //{
            //    UserName = model.Email,
            //    FirstName = model.Firstname,
            //    LastName = model.Lastname,
            //    Email = model.Email,
            //    Image = img,
            //    PhoneNumber = model.phoneNumber,
            //    PasswordHash = model.Password,
            //};
            
            user.Image = img;

            //for (int i = 1; i <= model.Permissions.Count; i++)
            //{
            //    user.Permissions.Add(new PermissionsEntity() { RoleId = i, UserId = user.Id });
            //}

            //user.Permissions.Add(new PermissionsEntity() { RoleId = int.Parse(model.Permissions[2].RoleName), UserId = user.Id });
            var a = 1 >= 0 ? false : true;
            var result = await _userRepository.RegisterUserAsync(user, model.Password);
            //user.Permissions.Add(new PermissionsEntity() { RoleId = model.Role, UserId = user.Id });
            if (result.Succeeded)
            {
                //foreach (var item in model.Permissions)
                //{
                //    await _userRepository.AddRoleAsync(user, item.RoleName);
                //}
                var role = model.Role != null ? model.Role : "User";
                await _userRepository.AddRoleAsync(user, role); //Roles.User
                
                //await _userRepository.AddRoleAsync(user, Roles.PermanentUser); //Roles.User

            }
            else return new ServiceResponse
            {
                Message = "Is not successed!",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
            var token = _jwtTokenService.CreateTokenAsync(user);
            return new ServiceResponse
            {
                Message = "User successfully created!",
                Payload = token.Result,//token.Result,
                IsSuccess = true
            };

            
        }

        public async Task<ServiceResponse> LoginUserAsync(LoginDTO model)
        {
            var user = await _userRepository.GetUserByEmailAsync(model.Email);


            if (user == null)
            {
                return new ServiceResponse
                {
                    Message = "The user is not registered.",
                    IsSuccess = false
                };
            }

            var result = await _userRepository.ValidatePasswordAsync(model.Email, model.Password);
            if (!result)
            {
                return new ServiceResponse
                {
                    Message = "Incorrect password.",
                    IsSuccess = false
                };
            }

            var tokens = await _jwtTokenService.CreateTokenAsync(user);

            return new ServiceResponse
            {
                Message = "Authentication was successful.",
                Payload = tokens,
                IsSuccess = true,
            };
        }

        public async Task<ServiceResponse> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
                return new ServiceResponse
                {
                    IsSuccess = false,
                    Message = "User does not exist."
                };

            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userRepository.ConfirmEmailAsync(user, normalToken);

            if (result.Succeeded)
                return new ServiceResponse
                {
                    Message = "The email was successfully verified!",
                    IsSuccess = true,
                };

            return new ServiceResponse
            {
                IsSuccess = false,
                Message = "The email has not been verified.",
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        public async Task<ServiceResponse> EditUserAsync(UserEditDTO model)
        {
            var oldUser = await _userRepository.GetUserByIdAsync(model.id.ToString());
            var user = _mapper.Map(model, oldUser);


            if (user != null)
            {
                //var img = await ImageHelper.SaveImageAsync(model.Image, DirectoriesInProject.UserImages);
                if (model.Image != null)
                {
                    string param = oldUser.Image.ToString();
                    ImageHelper.DeleteImage(oldUser.Image, DirectoriesInProject.UserImages);
                    user.Image = await ImageHelper.SaveImageAsync(model.Image, DirectoriesInProject.UserImages);
                }
                var checkEmail = await _userRepository.GetUserByEmailAsync(user.Email);
                if (checkEmail !=null && checkEmail.Id != model.id) 
                {
                    return new ServiceResponse
                    {
                        Message = "Email already exists",
                        IsSuccess = false
                    };
                }
                if(model.Role != null)
                {
                    var oldRole = await _userRepository.GetRolesAsync(oldUser);
                    await _userRepository.RemoveRoleAsync(user, oldRole);
                    var role = model.Role != null ? model.Role : "User";
                    await _userRepository.AddRoleAsync(user, role);
                }                

                await _userRepository.UpdateUserAsync(user);
                return new ServiceResponse
                {
                    Message = "The user was successfully updated!",
                    IsSuccess = true,
                    Payload = "ok"
                };
            }

            return new ServiceResponse
            {
                Message = "The user was not successfully updated!",
                IsSuccess = false,
            };
        }
        public async Task<ServiceResponse> GetAllUsersAsync()
        {
            var users = _userRepository.GetAllUsersAsync().Result.Select(user => new UserItemDTO
            {
                Id = user.Id,
                Firstname = user.FirstName,
                Lastname = user.LastName,
                Email = user.Email,
                phoneNumber = user.PhoneNumber,
                Image = user.Image,
                Permissions = _userRepository.GetRolesAsync(user).Result.Select(perm => new PermissionItemDTO { RoleName = perm }).ToList(),
            }).ToList();
            return new ServiceResponse
            {
                Message = "All users successfully loaded.",
                IsSuccess = true,
                Payload = users
            };
        }
        public async Task<ServiceResponse> GetUserByIdAsync(int id)
        {
            //var user = _userRepository.GetUserByIdAsync(id.ToString());
            var users = _userRepository.GetAllUsersAsync().Result.Where(user => user.Id == id)
            .Select(user => new UserItemDTO
            {
                Id = user.Id,
                Firstname = user.FirstName,
                Lastname = user.LastName,
                Email = user.Email,
                phoneNumber = user.PhoneNumber,
                Image = user.Image,
                Permissions = _userRepository.GetRolesAsync(user).Result.Select(perm => new PermissionItemDTO { RoleName = perm }).ToList(),
            }).ToList().First();

            if (users != null)
            {
                return new ServiceResponse()
                {
                    Payload = users,
                    Message = "User found",
                    IsSuccess = true,
                };
            }
            return new ServiceResponse()
            {
                Message = "User does not exist",
                IsSuccess = false,
            };            
        }
        public async Task<ServiceResponse> DeleteUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id.ToString());
            if (user != null) 
            {
                ImageHelper.DeleteImage(user.Image, DirectoriesInProject.UserImages);
                await _userRepository.DeleteUserAsync(user);

                return new ServiceResponse()
                {
                    IsSuccess = false,
                    Message = "User was delete"
                };
            }         
            return new ServiceResponse()
            {
                IsSuccess = true,
                Message = "User does not exist"
            };
        }
    }
}
