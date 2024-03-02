using Microsoft.AspNetCore.Http;

namespace Infrastructure.MyShop.Models.DTO.CategoryDTO
{
    public class CategoryCreateDTO
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
    }
}
