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

        //public ICollection<Product> GetProductsAsync(GetProductsVM model)
        //{
        //    int start = model.pageNumber * model.pageSize;
        //    int end = model.pageSize * model.pageNumber + model.pageSize;
        //    return FilterByName(GetByCategoryName(model.Category), model.Find).Skip(start).Take(end - start).ToList();
        //}
        public ICollection<ProductEntity> GetProductsAsync()
        {
            //return GetAll().Include(i => i.Category).Include(i => i.VariantProducts).ToList();
            return GetAll().Include(i => i.Category).Include(i => !i.IsDelete).ToList();
        }


        //public async Task RemoveVariantProductsAsync(int productId)
        //{
        //    var vrs = await _dbContext.VariantProduct.Where(vp => vp.ProductId == productId).ToListAsync();
        //    _dbContext.VariantProduct.RemoveRange(vrs);
        //}

        //public async Task AddVariantProductsToProductAsync(int productId, List<int> variantsIds)
        //{
        //    foreach (var variantId in variantsIds)
        //    {
        //        var real_variant = await _dbContext.Variant.FindAsync(variantId);

        //        if (real_variant != null)
        //        {
        //            _dbContext.VariantProduct.Add(new VariantProduct { VariantId = variantId, ProductId = productId });
        //        }
        //    }
        //}

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
