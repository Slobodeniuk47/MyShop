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

        public async Task<ProductEntity> GetByName(string name)
        {
            return await _dbContext.Set<ProductEntity>().Include(i => i.Category)
                .AsNoTracking().FirstOrDefaultAsync(e => e.Name == name);
        }
        public ICollection<ProductEntity> GetProductsAsync()
        {
            return GetAll().Include(i => i.Category).Include(i => !i.IsDelete).ToList();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
