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
            //var result = await _context.Categories.Select(x => _mapper.Map<CategoryItemDTO>(x)).ToListAsync();
            //return Ok(result);
            var result = await _context.Categories

                .Select(x => new CategoryItemDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ParentId = x.ParentId,
                    ParentName = x.Parent.Name,
                    Image = x.Image
                }).ToListAsync();
            return Ok(result);
        }
        [HttpGet("get/{id}")]
        public async Task<ActionResult<IEnumerable<CategoryItemDTO>>> GetById(int id)
        {
            var result = await _context.Categories
                .Where(x => x.Id == id)
                .Select(x => new CategoryItemDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ParentId = x.ParentId,
                    ParentName = x.Parent.Name,
                    Image = x.Image,
                    DateCreated = DateTime.SpecifyKind(x.DateCreated, DateTimeKind.Utc).ToString(),
                    DateUpdated = DateTime.SpecifyKind(x.DateUpdated, DateTimeKind.Utc).ToString(),
                    //countProducts
                    countProducts =
                      x.Products
                      .Where(x => x.CategoryId == id)
                      .Select(x => new ProductItemDTO
                      {
                          Id = x.Id
                      }).ToList().Count,
                    //Subcategories
                    Subcategories =
                      x.Subcategories
                      .Where(x => x.ParentId == id)
                      .Select(x =>
                          new CategoryItemDTO
                          {
                              Id = x.Id,
                              Name = x.Name,
                              ParentId = x.ParentId,
                              ParentName = x.Parent.Name,
                              Description = x.Description,
                              Image = x.Image
                          }).ToList(),
                    //Products
                    Products = x.Products
                      .Where(x => x.CategoryId == id)
                      .Select(x => new ProductItemDTO
                      {
                          Id = x.Id,
                          Name = x.Name,
                          Description = x.Description,
                          Price = x.Price,
                          CategoryId = x.CategoryId,
                          CategoryName = x.Category.Name,

                      }).ToList()
                }).ToListAsync();
            //var result = await _context.Categories.Where(x => x.Id == id).Select(x => _mapper.Map<CategoryItemDTO>(x)).ToListAsync();
            if (result.Count > 0)
            {
                return new ObjectResult(result[0]);
            }
            else { return NotFound(); }
        }
        //public async Task<List<CategoryEntity>> getSubcategoriesFromCategory(ICollection<CategoryEntity> subcategories)
        //{
        //    var categories = await _context.Categories.Include(c => c.Subcategories).ToListAsync();

        //    var categ_list = new List<CategoryEntity>();

        //    foreach (var categ_tmp in subcategories)
        //    {
        //        var sub_category = categories.Find(c => c.Id == categ_tmp.Id);
        //        if (sub_category.Subcategories != null)
        //        {
        //            var allSubCategories = await getSubcategoriesFromCategory(sub_category.Subcategories);
        //            categ_list.AddRange(allSubCategories);
        //        }
        //        categ_list.Add(categ_tmp);
        //    }

        //    return categ_list;
        //}
        //public async Task<List<CategoryItemDTO>> GetAllSubcategoriesByCategoryId(int id)
        //{
        //    var categories = await _context.Categories.Include(c => c.Subcategories).ToListAsync();
        //    var category = categories.Find(c => c.Id == id);

        //    var categ_list = new List<CategoryEntity>();

        //    if (category != null)
        //    {
        //        foreach (var categ_tmp in category.Subcategories)
        //        {
        //            var sub_category = categories.Find(c => c.Id == categ_tmp.Id);
        //            if (sub_category.Subcategories.Count != 0)
        //            {
        //                var allSubCategories = await getSubcategoriesFromCategory(sub_category.Subcategories);
        //                categ_list.AddRange(allSubCategories);
        //            }
        //            categ_list.Add(categ_tmp);
        //        }

        //        var final_list = _mapper.Map<List<CategoryEntity>, List<CategoryItemDTO>>(categ_list);

        //        return final_list;
        //    }
        //    return null;
        //}
        //[HttpGet("GetMainCategories")]
        //public async Task<ActionResult<IEnumerable<CategoryItemDTO>>> GetMainCategories()
        //{
        //    var products = _context.Products.Select(x => new ProductItemDTO {
        //        Id = x.Id,
        //        Name = x.Name,
        //        Description = x.Description,
        //        Price = x.Price,
        //        CategoryId = x.CategoryId,
        //        CategoryName = x.Category.Name
        //    });
        //    var categories = await _context.Categories
        //        .Where(c => c.ParentId == null)
        //        .Include(c => c.Subcategories)
        //        .ToListAsync();

        //    foreach (var category in categories)
        //    {
        //        var categoriesWithAllSubCategories = await GetAllSubcategoriesByCategoryId(category.Id);

        //        var subcategoryIds = categoriesWithAllSubCategories.Select(cat => cat.Id).ToList();
        //        subcategoryIds.Add(category.Id);

        //        var productCount = products.Count(prod => subcategoryIds.Contains((int)prod.CategoryId));
        //        category.CountOfProducts = productCount;
        //    }

        //    var result = _mapper.Map<List<CategoryEntity>, List<CategoryItemDTO>>(categories);

        //    return Ok(result);
        //}
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
