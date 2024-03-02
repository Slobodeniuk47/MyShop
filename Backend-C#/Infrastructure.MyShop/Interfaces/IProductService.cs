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
        Task<ServiceResponse> GetProductsAsync();
        Task<ServiceResponse> GetProductByNameAsync(string name);
        Task<ServiceResponse> GetProductByIdAsync(int id);
        Task<ServiceResponse> GetProductsByCategoryId(int id);
        Task<ServiceResponse> CreateProductAsync(ProductCreateDTO model);
        //Task<ServiceResponse> GetProductCountAsync();
        //Task<List<ProductImageEntity>> GetProductImagesByProductIdAsync(int id);
        Task<ServiceResponse> EditProductAsync(ProductEditDTO model);
        Task<ServiceResponse> DeleteProductAsync(int id);

    }
}
