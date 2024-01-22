using Microsoft.AspNetCore.Http;

namespace Infrastructure.MyShop.Models.DTO.CategoryDTO
{
    public class CategoryEditDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile? ImageUpload { get; set; } // IFormFILE || null
        public string Description { get; set; }
        public int? ParentId { get; set; } // ParentId || null
    }
}
