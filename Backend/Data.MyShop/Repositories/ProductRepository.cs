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
    public class ProductRepository : GenericRepository<ProductEntity>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IQueryable<ProductEntity> Products => GetAll();

        public Task<IQueryable<ProductEntity>> SearchProducts(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductEntity> GetByName(string name)
        {
            return await _dbContext.Set<ProductEntity>().Include(i => i.Category)
                .AsNoTracking().FirstOrDefaultAsync(e => e.Name == name);
        }

        private ICollection<ProductEntity> GetByCategoryName(string category)
        {
            if (category == "")
            {
                return GetAll().Include(i => i.Category).ToList();
            }

            return _dbContext.Categories.FirstOrDefault(i => i.Name == category)?.Products;
        }

        private ICollection<ProductEntity> FilterByName(ICollection<ProductEntity> list, string name)
        {
            return list.Where(i => i.Name.ToLower().Contains(name.ToLower())).ToList();
        }
        public ICollection<ProductEntity> GetProductsAsync()
        {
            return GetAll().Include(i => i.Category).Include(i => !i.IsDelete).ToList();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveProductImagesAsync(int productId)
        {
            var imgs = await _dbContext.ProductImages.Where(prodImg => prodImg.ProductId == productId).ToListAsync();
            _dbContext.ProductImages.RemoveRange(imgs);
        }
    }
}
