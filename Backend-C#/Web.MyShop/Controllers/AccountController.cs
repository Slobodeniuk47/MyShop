﻿using AutoMapper;
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
            var result = await _userService.GetAllUsersAsync();
            return Ok(result);
        }
        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _userService.GetUserByIdAsync(id);
            return Ok(result);
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromForm] UserCreateDTO model)
        {
            var result = await _userService.RegisterUserAsync(model);
            return Ok(result);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromForm] LoginDTO model)
        {
            var result = await _userService.LoginUserAsync(model);
            return Ok(result);
        }
        [HttpPost]
        [Route("GoogleExternalLogin")]
        public async Task<IActionResult> GoogleExternalLogin([FromForm] ExternalLoginDTO model)
        {
            var result = await _userService.GoogleExternalLogin(model);
            return Ok(result);
        }
        [HttpPut]
        [Route("edit")]
        public async Task<IActionResult> Edit([FromForm] UserEditDTO model)
        {
            var result = await _userService.EditUserAsync(model);
            return Ok(result);
        }
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _userService.DeleteUserByIdAsync(id);
            return Ok(result);
        }
    }
}