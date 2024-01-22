using Infrastructure.MyShop.Models.DTO.CommentDTO;

namespace Infrastructure.MyShop.Models.DTO.ProductDTO
{
    public class ProductItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string DateCreated { get; set; }
        public string DateUpdated { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<ProductImageItemDTO> Images { get; set; }
        public List<CommentItemDTO> Comments { get; set; }
        public int NumberOfComments { get; set; }
        public int Stars { get; set; }
    }
}
