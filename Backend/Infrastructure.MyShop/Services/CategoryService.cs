using AutoMapper;
using Data.MyShop.Constants;
using Data.MyShop.Entities;
using Data.MyShop.Interfaces;
using Infrastructure.MyShop.Helpers;
using Infrastructure.MyShop.Interfaces;
using Infrastructure.MyShop.Models.DTO.CategoryDTO;
using Infrastructure.MyShop.Models.DTO.ProductDTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MyShop.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper, IProductRepository productService)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _productRepository = productService;
        }
        public async Task<ServiceResponse> GetAllAsync()
        {
            try
            {
                var categories = await _categoryRepository.Categories

                    .Select(c => new CategoryItemDTO
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description,
                        ParentId = c.ParentId,
                        ParentName = c.Parent.Name,
                        Image = c.Image,
                        DateCreated = DateTime.SpecifyKind(c.DateCreated, DateTimeKind.Utc).ToString(),
                        DateUpdated = DateTime.SpecifyKind(c.DateUpdated, DateTimeKind.Utc).ToString(),
                        //countProducts
                        countProducts =
                          c.Products
                          .Where(p => p.CategoryId == c.Id)
                          .Select(p => new ProductItemDTO
                          {
                              Id = p.Id
                          }).ToList().Count,
                    }).ToListAsync();

                return new ServiceResponse
                {
                    IsSuccess = true,
                    Payload = categories
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ServiceResponse> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.Categories
                .Where(c => c.Id == id)
                .Select(c => new CategoryItemDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    ParentId = c.ParentId,
                    ParentName = c.Parent.Name,
                    Image = c.Image,
                    DateCreated = DateTime.SpecifyKind(c.DateCreated, DateTimeKind.Utc).ToString(),
                    DateUpdated = DateTime.SpecifyKind(c.DateUpdated, DateTimeKind.Utc).ToString(),
                    //countProducts
                    countProducts =
                      c.Products
                      .Where(p => p.CategoryId == id)
                      .Select(x => new ProductItemDTO
                      {
                          Id = c.Id
                      }).ToList().Count,
                    //Subcategories
                    Subcategories =
                      c.Subcategories
                      .Where(c => c.ParentId == id)
                      .Select(s =>
                          new CategoryItemDTO
                          {
                              Id = s.Id,
                              Name = s.Name,
                              ParentId = s.ParentId,
                              ParentName = s.Parent.Name,
                              Description = s.Description,
                              Image = s.Image
                          }).ToList(),
                    //Products
                    Products = c.Products
                      .Where(c => c.CategoryId == id)
                      .Select(p => new ProductItemDTO
                      {
                          Id = p.Id,
                          Name = p.Name,
                          Description = p.Description,
                          Price = p.Price,
                          CategoryId = p.CategoryId,
                          CategoryName = p.Category.Name,

                      }).ToList()
                }).ToListAsync();

            if (category != null)
            {
                return new ServiceResponse
                {
                    IsSuccess = true,
                    Payload = category
                };
            }

            return new ServiceResponse
            {
                IsSuccess = false,
                Message = "Category not found"
            };
        }

        //public async Task<ServiceResponse> GetMainCategoriesAsync()
        //{
        //    try
        //    {
        //        var products = _productRepository.GetAll();
        //        var categories = await _categoryRepository.Categories
        //            .Where(c => c.ParentId == null)
        //            .Include(c => c.Subcategories)
        //            .ToListAsync();

        //        foreach (var category in categories)
        //        {
        //            var categoriesWithAllSubCategories = await GetAllSubcategoriesByCategoryId(category.Id);

        //            var subcategoryIds = categoriesWithAllSubCategories.Select(cat => cat.Id).ToList();
        //            subcategoryIds.Add(category.Id);

        //            var productCount = products.Count(prod => subcategoryIds.Contains((int)prod.CategoryId));
        //            //category.CountOfProducts = productCount;
        //        }

        //        var categoriesWithParents = _categoryRepository.Categories.Include(c => c.Parent).ToList();

        //        //categories.ForEach(cat => cat.CountOfProducts = products.Where(prod=>prod.CategoryId == cat.Id).Count());

        //        var categoryVMs = _mapper.Map<List<CategoryEntity>, List<CategoryItemDTO>>(categories);




        //        return new ServiceResponse
        //        {
        //            IsSuccess = true,
        //            Payload = categoryVMs
        //        };

        //    }
        //    catch (Exception ex)
        //    {

        //        return new ServiceResponse
        //        {
        //            IsSuccess = false,
        //            Message = ex.Message
        //        };
        //    }
        //}
        //public async Task<ServiceResponse> GetNearSubcategoriesByCategoryId(int id)
        //{
        //    try
        //    {
        //        var categories = await _categoryRepository.Categories.Include(c => c.Subcategories).ToListAsync();
        //        var category = categories.Find(c => c.Id == id);
        //        //var categoriesWithParents = _categoryRepository.Categories.Include(c => c.Parent).ToList();

        //        var categoryVM = _mapper.Map<CategoryEntity, CategoryItemDTO>(category);


        //        return new ServiceResponse
        //        {
        //            IsSuccess = true,
        //            Payload = categoryVM
        //        };

        //    }
        //    catch (Exception ex)
        //    {

        //        return new ServiceResponse
        //        {
        //            IsSuccess = false,
        //            Message = ex.Message
        //        };
        //    }
        //}

        //public async Task<List<CategoryItemDTO>> GetAllSubcategoriesByCategoryId(int id)
        //{
        //    var categories = await _categoryRepository.Categories.Include(c => c.Subcategories).ToListAsync();
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

        //public async Task<List<CategoryEntity>> getSubcategoriesFromCategory(ICollection<CategoryEntity> subcategories)
        //{
        //    var categories = await _categoryRepository.Categories.Include(c => c.Subcategories).ToListAsync();

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

        public async Task<ServiceResponse> Create(CategoryCreateDTO model)
        {
            var category = _mapper.Map<CategoryEntity>(model);
            category.ParentId = model.ParentId == 0 ? null : model.ParentId;
            category.Image = await ImageHelper.SaveImageAsync(model.Image, DirectoriesInProject.CategoryImages);
            await _categoryRepository.Create(category);

            return new ServiceResponse
            {
                Message = "Category was created",
                IsSuccess = true,
            };
        }

        public async Task<ServiceResponse> EditCategoryAsync(CategoryEditDTO model)
        {
            var category = await _categoryRepository.Categories.SingleOrDefaultAsync(x => x.Id == model.Id);

            if (category == null)
            {
                return new ServiceResponse()
                {
                    Message = "Upload category is not correct, upload is closed",
                    IsSuccess = false,
                };
            }

            if (model.ImageUpload != null)
            {
                ImageHelper.DeleteImage(category.Image, DirectoriesInProject.CategoryImages);
                category.Image = await ImageHelper.SaveImageAsync(model.ImageUpload, DirectoriesInProject.CategoryImages);
            }

            category.Name = model.Name;
            category.ParentId = model.ParentId == 0 ? null : model.ParentId;
            category.Description = model.Description;
            category.DateUpdated = DateTime.UtcNow;

            await _categoryRepository.Update(category);

            return new ServiceResponse()
            {
                Message = "Category update was successful",
                IsSuccess = true,
            };

        }
        public async Task<ServiceResponse> DeleteCategoryAsync(int id)
        {
            var category = await _categoryRepository.Categories.SingleOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                return new ServiceResponse()
                {
                    Message = "Uploaded category is not correct, uploaded is closed",
                    IsSuccess = false,
                };
            }

            ImageHelper.DeleteImage(category.Image, DirectoriesInProject.CategoryImages);



            await _categoryRepository.Delete(category);
            return new ServiceResponse()
            {
                Message = "Сategory has been deleted",
                IsSuccess = true,
            };
        }
    }
}
