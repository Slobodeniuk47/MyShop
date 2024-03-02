using Infrastructure.MyShop.Interfaces;
using Infrastructure.MyShop.Models.DTO.AccountDTO;
using Infrastructure.MyShop.Models.DTO.RoleDTO;
using Infrastructure.MyShop.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService) 
        {
            _roleService = roleService;
        }
        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _roleService.GetAllRolesAsync();
            return Ok(result);
        }
        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _roleService.GetRoleByIdAsync(id);
            return Ok(result);
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromForm] RoleCreateDTO model)
        {
            var result = await _roleService.CreateRoleAsync(model);
            return Ok(result);
        }
        [HttpPut]
        [Route("edit")]
        public async Task<IActionResult> Edit([FromForm] RoleEditDTO model)
        {
            var result = await _roleService.EditRoleAsync(model);
            return Ok(result);
        }
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _roleService.DeleteRoleByIdAsync(id);
            return Ok(result);
        }
    }
}
