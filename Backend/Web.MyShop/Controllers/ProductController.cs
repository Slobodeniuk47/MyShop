using AutoMapper;
using Data.MyShop.Entities;
using Data.MyShop;
using Infrastructure.MyShop.Helpers;
using Infrastructure.MyShop.Models.DTO.ProductDTO;
using Microsoft.AspNetCore.Mvc;
using Data.MyShop.Constants;
using Microsoft.EntityFrameworkCore;
using Infrastructure.MyShop.Models.DTO;
using Infrastructure.MyShop.Models.DTO.CommentDTO;
using Infrastructure.MyShop.Interfaces;

namespace Web.MyShop.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _iconfiguration;
        private readonly IProductService _productService;
        public ProductController(ApplicationDbContext context, IConfiguration iconfigoration, IMapper mapper, IProductService productService)
        {
            _context = context;
            _mapper = mapper;
            _iconfiguration = iconfigoration;
            _productService = productService;
        }

        [HttpGet("get")]
        public async Task<ActionResult<IEnumerable<ProductItemDTO>>> Get()
        {
            var result = await _productService.GetProductsAsync();
            return Ok(result);
        }
        [HttpGet("getProductsByCategoryId/{id:int}")]
        public async Task<ActionResult<IEnumerable<ProductItemDTO>>> GetProductsByCategoryId(int id)
        {
            var result = await _productService.GetProductsByCategoryId(id);
            return Ok(result);
        }
        [HttpGet("get/{id:int}")]
        public async Task<ActionResult<IEnumerable<ProductItemDTO>>> GetById(int id)
        {
            var result = await _productService.GetProductByIdAsync(id);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] ProductCreateDTO model)
        {
            var result = await _productService.CreateProductAsync(model);
            return Ok(result);
        }

        [HttpPut("edit")]
        public async Task<ActionResult<IEnumerable<ProductEditDTO>>> Edit([FromForm] ProductEditDTO model)
        {
            //var oldProduct = await _context.Products.SingleOrDefaultAsync(x => x.Id == model.Id);
            //var product = _mapper.Map(model, oldProduct);
            //if (product == null)
            //    return NotFound();
            //if (model.ImagesUpload != null)
            //{
            //    try
            //    {
            //        // Delete images
            //        foreach (var img in await GetProductImagesByProductIdAsync(product.Id))
            //        {
            //            product.ProductImages.Remove(img);
            //            ImageHelper.DeleteImage(img.Name, DirectoriesInProject.ProductImages);
            //        }
            //        // Save Images
            //        foreach (IFormFile item in model.ImagesUpload)
            //        {
            //            string img = await ImageHelper.SaveImageAsync(item, DirectoriesInProject.ProductImages);
            //            var productImage = new ProductImageEntity()
            //            {
            //                Name = img,
            //                ProductId = product.Id
            //            };
            //            _context.ProductImages.Add(productImage);
            //        }
            //    }
            //    catch { }         
            //}
            //product.DateUpdated = DateTime.UtcNow;

            //await _context.SaveChangesAsync();
            var result = await _productService.EditProductAsync(model);
            return Ok(result);
        }
        [HttpDelete("deleteProduct/{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            //var product = await _context.Products.SingleOrDefaultAsync(x => x.Id == id);
            //if (product == null)
            //    return NotFound();
            //// Delete images
            //foreach (var img in await GetProductImagesByProductIdAsync(product.Id))
            //{
            //    product.ProductImages.Remove(img);
            //    ImageHelper.DeleteImage(img.Name, DirectoriesInProject.ProductImages);
            //}
            //_context.Products.Remove(product);
            //await _context.SaveChangesAsync();
            var result = await _productService.DeleteProductAsync(id);
            return Ok(result);
        }

        //Task<List<ProductImageEntity>> GetProductImagesByProductIdAsync(int productId)
        //{
        //    var productImages = _context.ProductImages.Where(x => x.ProductId == productId).ToListAsync();
        //    return productImages;
        //}
        //[HttpGet("getProductImagesByProductId/{id}")]
        //public async Task<ActionResult<IEnumerable<ProductImageItemDTO>>> GetImagesByProductId(int id)
        //{
        //    var result = await _productService.GetProductImagesByProductIdAsync(id);
        //    return Ok(result);
        //}
        [HttpPost("uploadProductImage")]
        public async Task<ActionResult<IEnumerable<ImageUploadDTO>>> UploadImage([FromForm] ImageUploadDTO model)
        {
            if (model.Image != null)
            {
                var entity = new ProductImageEntity();
                try
                {
                   
                    entity.DateCreated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
                    entity.DateUpdated = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
                    entity.ProductId = model.ProductId;

                    var product = await _context.Products.SingleOrDefaultAsync(x => x.Id == model.ProductId);
                    entity.Name = await ImageHelper.SaveImageAsync(model.Image, DirectoriesInProject.ProductImages);//ImageHelper.SaveAndGetImageName(model.Image, _iconfiguration, DirectoriesInProject.ProductImages);

                    _context.ProductImages.Add(entity);
                    _context.SaveChanges();
                }
                catch { }
                

                return Ok(entity);
            }
            return BadRequest();
        }
        [HttpDelete("RemoveProductImage/{id}")]
        public async Task<IActionResult> RemoveImage(int id)
        {
            var image = await _context.ProductImages.SingleOrDefaultAsync(x => x.Id == id);
            var product = await _context.Products.SingleOrDefaultAsync(x => x.Id == image.ProductId);
            ImageHelper.DeleteImage(image.Name, DirectoriesInProject.ProductImages);

            _context.ProductImages.Remove(image);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("getProductImages")]
        public async Task<ActionResult<IEnumerable<ProductImageItemDTO>>> GetImages()
        {
            var result = await _context.ProductImages.Select(x =>
                            new ProductImageItemDTO { Id = x.Id, Name = x.Name, ProductId = x.ProductId, ProductName = x.Product.Name }).ToListAsync();
            return Ok(result);
        }
        [HttpGet("getProductImage/{id:int}")]
        public async Task<ActionResult<IEnumerable<ProductImageItemDTO>>> GetImageById(int id)
        {
            var result = await _context.ProductImages.Where(x => x.Id == id).Select(x =>
                            new ProductImageItemDTO { Id = x.Id, Name = x.Name, ProductId = x.ProductId, ProductName = x.Product.Name }).ToListAsync();
            if (result.Count > 0)
            {
                return new ObjectResult(result[0]);
            }
            else { return NotFound(); }
        }

    }
}
