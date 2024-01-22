using Data.MyShop.Entities;

namespace Data.MyShop.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<CategoryEntity, int>
    {
        IQueryable<CategoryEntity> Categories { get; }
    }
}
