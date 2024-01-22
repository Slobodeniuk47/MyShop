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
        Task<ServiceResponse> Create(CategoryCreateDTO model);
        Task<ServiceResponse> GetAllAsync();
        Task<CategoryEntity> GetByIdAsync(int id);
        Task<ServiceResponse> DeleteCategoryAsync(int id);
        Task<ServiceResponse> GetMainCategoriesAsync();
        Task<ServiceResponse> GetNearSubcategoriesByCategoryId(int id);
        Task<List<CategoryItemDTO>> GetAllSubcategoriesByCategoryId(int id);
        //public Task<ICollection<Options>> GetCategoryOptionsAsyncByCategoryId(int id);
        Task<ServiceResponse> EditCategoryAsync(CategoryEditDTO model);
    }
}
