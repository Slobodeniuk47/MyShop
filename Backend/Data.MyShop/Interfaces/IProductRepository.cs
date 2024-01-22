﻿using Data.MyShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.MyShop.Interfaces
{
    public interface IProductRepository : IGenericRepository<ProductEntity, int>
    {
        IQueryable<ProductEntity> Products { get; }
        Task<IQueryable<ProductEntity>> SearchProducts(string name);
        Task<ProductEntity> GetByName(string name);
        //ICollection<Product> GetProductsAsync(GetProductsVM model);
        ICollection<ProductEntity> GetProductsAsync();

        //public Task RemoveVariantProductsAsync(int productId);
        public Task RemoveProductImagesAsync(int productId);

        //public Task AddVariantProductsToProductAsync(int productId, List<int> variantsIds);

        public Task SaveChangesAsync();

    }
}
