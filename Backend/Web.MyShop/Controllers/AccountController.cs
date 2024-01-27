using AutoMapper;
using Data.MyShop.Constants;
using Data.MyShop.Entities.Identity;
using Infrastructure.MyShop.Helpers;
using Infrastructure.MyShop.Interfaces;
using Infrastructure.MyShop.Models.DTO.AccountDTO;
using Infrastructure.MyShop.Models.DTO.CommentDTO;
using Infrastructure.MyShop.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Web.MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetAll()
        {
            var result = _userService.GetAllUsersAsync().Result;
            return Ok(result);
        }
        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = _userService.GetUserByIdAsync(id).Result;
            return Ok(result);
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromForm] UserCreateDTO model)
        {
            var result = _userService.RegisterUserAsync(model).Result;
            return Ok(result);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromForm] LoginDTO model)
        {
            var result = _userService.LoginUserAsync(model).Result;
            return Ok(result);
        }
        [HttpPut]
        [Route("edit")]
        public async Task<IActionResult> Edit([FromForm] UserEditDTO model)
        {
            var result = _userService.EditUserAsync(model).Result;
            return Ok(result);
        }
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = _userService.DeleteUserByIdAsync(id).Result;
            return Ok(result);
        }
    }
}