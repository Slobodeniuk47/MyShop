using Infrastructure.MyShop.Models.DTO.ProductDTO;

namespace Infrastructure.MyShop.Models.DTO.CategoryDTO
{
    public class CategoryItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string DateCreated { get; set; }
        public string DateUpdated { get; set; }
        public int? ParentId { get; set; }
        public string ParentName { get; set; }
        public int countProducts { get; set; }
        public List<CategoryItemDTO> Subcategories { get; set; }
        public List<ProductItemDTO> Products { get; set; }
    }
}
