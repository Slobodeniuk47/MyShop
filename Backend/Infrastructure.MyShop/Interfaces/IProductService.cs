using Data.MyShop.Entities;
using Infrastructure.MyShop.Models.DTO.CategoryDTO;
using Infrastructure.MyShop.Models.DTO.ProductDTO;
using Infrastructure.MyShop.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MyShop.Interfaces
{
    public interface IProductService
    {
        Task<ServiceResponse> GetProductAsync(string name);
        Task<ServiceResponse> GetProductByIdAsync(int id);
        Task<ServiceResponse> CreateProductAsync(ProductCreateDTO model);
        Task<ServiceResponse> GetProductsAsync(ProductItemDTO model);
        Task<ServiceResponse> GetProductCountAsync();
        Task<ServiceResponse> GetProductsAsync();
        Task<ServiceResponse> EditProductAsync(ProductEditDTO model);
        //Task<ServiceResponse> GetProductByCategoryIdWithPagination(GetProductsWithPaginationAndByCategoryIdDTO model);
        Task DeleteProductAsync(int id);
        //Task<ServiceResponse> GetProductWithLimitByCategoryIdAsync(RecomendedProductDTO model);
        //Task<ServiceResponse> GetProductByFiltersAsync(FilterVM model);
        //Task<ServiceResponse> GetProductWithLimitByUserIdAsync(GetProductsWithPaginationAndByUserIdDTO model);

    }
}
