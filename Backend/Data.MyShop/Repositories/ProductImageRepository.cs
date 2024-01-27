using Data.MyShop.Entities;
using Data.MyShop.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.MyShop.Repositories
{
    public class ProductImageRepository : GenericRepository<ProductImageEntity>, IProductImageRepository
    {
        public ProductImageRepository(ApplicationDbContext context) : base(context)
        {
        }
        public IQueryable<ProductImageEntity> ProductImages => GetAll();
        public async Task<List<ProductImageEntity>> GetProductImagesByProductIdAsync(int id)
        {
            var productImages = await ProductImages.Where(x => x.ProductId == id).ToListAsync();
            return productImages;
        }
        public async Task RemoveProductImagesByProductIdAsync(int productId)
        {
            var imgs = await _dbContext.ProductImages.Where(prodImg => prodImg.ProductId == productId).ToListAsync();
            _dbContext.ProductImages.RemoveRange(imgs);
        }
    }
}
