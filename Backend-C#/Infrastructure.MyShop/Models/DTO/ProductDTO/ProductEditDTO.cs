using Microsoft.AspNetCore.Http;

namespace Infrastructure.MyShop.Models.DTO.ProductDTO
{
    public class ProductEditDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public List<IFormFile>? ImagesUpload { get; set; }
        public int? CategoryId { get; set; }

    }
}
