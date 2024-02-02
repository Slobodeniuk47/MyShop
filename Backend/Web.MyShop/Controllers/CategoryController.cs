using AutoMapper;
using Data.MyShop.Entities;
using Data.MyShop;
using Infrastructure.MyShop.Models.DTO.CategoryDTO;
using Infrastructure.MyShop.Models.DTO.ProductDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Data.MyShop.Constants;
using Infrastructure.MyShop.Helpers;
using Infrastructure.MyShop.Interfaces;

namespace Web.MyShop.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _iconfiguration;
        private readonly ICategoryService _categoryService;

        public CategoryController(ApplicationDbContext context, IConfiguration iconfigoration, IMapper mapper, ICategoryService categoryService)
        {
            _context = context;
            _mapper = mapper;
            _iconfiguration = iconfigoration;
            _categoryService = categoryService;
        }

        [HttpGet("get")]
        public async Task<ActionResult<IEnumerable<CategoryItemDTO>>> Get()
        {
            var result = await _categoryService.GetAllAsync();
            return Ok(result);
        }
        [HttpGet("getMainCategories")]
        public async Task<ActionResult<IEnumerable<CategoryItemDTO>>> GetMainCategoriesAsync()
        {
            var result = await _categoryService.GetMainCategoriesAsync();
            return Ok(result);
        }
        [HttpGet("get/{id}")]
        public async Task<ActionResult<IEnumerable<CategoryItemDTO>>> GetById(int id)
        {
            var result = await _categoryService.GetByIdAsync(id);
            return Ok(result);
        }       
        [HttpPost("create")]
        public async Task<ActionResult<IEnumerable<CategoryCreateDTO>>> Create([FromForm] CategoryCreateDTO model)
        {
            var result = await _categoryService.Create(model);
            return Ok(result);

        }

        [HttpPut("edit")]
        public async Task<ActionResult<IEnumerable<CategoryEditDTO>>> Edit([FromForm] CategoryEditDTO model)
        {
            var result = await _categoryService.EditCategoryAsync(model);
            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);
            return Ok(result);
        }
    }
}
