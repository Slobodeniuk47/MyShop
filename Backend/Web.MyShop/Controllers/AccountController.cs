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
        private readonly IJwtTokenService _jwtTokenService;
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<RoleEntity> _roleManager;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public AccountController(IJwtTokenService jwtTokenService, IUserService userService, UserManager<UserEntity> userManager, RoleManager<RoleEntity> roleManager, IMapper mapper)
        {
            _userService = userService;
            _jwtTokenService = jwtTokenService;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = _userService.GetAllUsersAsync().Result;
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = _userService.GetUserByIdAsync(id).Result;
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromForm] UserCreateDTO model)
        {
            //var img = await ImageHelper.SaveImageAsync(model.Image, DirectoriesInProject.UserImages);

            //var user = _mapper.Map<UserEntity>(model);
            //user.Image = img;
            //var result = await _userManager.CreateAsync(user, model.Password);
            //if(result.Succeeded)
            //{
            //    result = await _userManager.AddToRoleAsync(user, Roles.User);

            //}
            //else return BadRequest(new { errors = result.Errors });
            //return Ok(new { token = _jwtTokenService.CreateTokenAsync(user) });
            try
            {
                var result = _userService.RegisterUserAsync(model).Result;
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromForm] LoginDTO model)
        {
            try
            {
                var result = _userService.LoginUserAsync(model).Result;
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("edit")]
        public async Task<IActionResult> Edit([FromForm] UserEditDTO model) //Have a bag! Image Delete
        {
            try
            {
                var result = _userService.EditUserAsync(model).Result;
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = _userService.DeleteUserByIdAsync(id).Result;
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}