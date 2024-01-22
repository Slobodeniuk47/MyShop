using Microsoft.AspNetCore.Http;

namespace Infrastructure.MyShop.Models.DTO.ProductDTO
{
    public class ProductCreateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public List<IFormFile> Images { get; set; }
        public int? CategoryId { get; set; }
    }
}
