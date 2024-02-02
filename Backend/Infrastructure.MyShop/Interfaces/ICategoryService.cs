using Data.MyShop.Entities;
using Infrastructure.MyShop.Models.DTO.CategoryDTO;
using Infrastructure.MyShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MyShop.Interfaces
{
    public interface ICategoryService
    {
        Task<ServiceResponse> GetAllAsync();
        Task<ServiceResponse> GetByIdAsync(int id);
        Task<ServiceResponse> GetMainCategoriesAsync();
        //Task<ServiceResponse> GetNearSubcategoriesByCategoryId(int id);
        //Task<List<CategoryItemDTO>> GetAllSubcategoriesByCategoryId(int id);
        Task<ServiceResponse> Create(CategoryCreateDTO model);
        Task<ServiceResponse> EditCategoryAsync(CategoryEditDTO model);
        Task<ServiceResponse> DeleteCategoryAsync(int id);
    }
}
