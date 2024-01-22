using Data.MyShop.Entities;
using Data.MyShop.Interfaces;
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
    }
}
