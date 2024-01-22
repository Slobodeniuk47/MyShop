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

namespace Web.MyShop.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _iconfiguration;

        public ProductController(ApplicationDbContext context, IConfiguration iconfigoration, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _iconfiguration = iconfigoration;
        }

        [HttpGet("get")]
        public async Task<ActionResult<IEnumerable<ProductItemDTO>>> Get()
        {
            var result = await _context.Products
                .Select(x => new ProductItemDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category.Name,
                    NumberOfComments = x.Comments.Count(),
                    DateCreated = DateTime.SpecifyKind(x.DateCreated, DateTimeKind.Utc).ToString(),
                    DateUpdated = DateTime.SpecifyKind(x.DateUpdated, DateTimeKind.Utc).ToString(),
                    Comments = x.Comments.Select(x => new CommentItemDTO { Id = x.Id, Title = x.Title, Message = x.Message, ProductId = x.ProductId, UserId = x.UserId, Stars = x.Stars, UserName = x.User.FirstName }).ToList(),
                    Images =
                        x.ProductImages
                        .Select(x =>
                            new ProductImageItemDTO { Id = x.Id, Name = x.Name, ProductId = x.ProductId, ProductName = x.Product.Name })
                        .ToList()
                }).ToListAsync();

            return Ok(result);
        }
        [HttpGet("getProductsByCategoryId/{id:int}")]
        public async Task<ActionResult<IEnumerable<ProductItemDTO>>> GetProductsByCategoryId(int id)
        {
            
            var result = await _context.Products
                .Where(x => x.CategoryId == id)
                .Select(x => new ProductItemDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category.Name,
                    NumberOfComments = x.Comments.Count(),
                    DateCreated = DateTime.SpecifyKind(x.DateCreated, DateTimeKind.Utc).ToString(),
                    DateUpdated = DateTime.SpecifyKind(x.DateUpdated, DateTimeKind.Utc).ToString(),
                    Comments = x.Comments.Select(x => new CommentItemDTO { Id = x.Id, Title = x.Title, Message = x.Message, ProductId = x.ProductId, UserId = x.UserId, Stars = x.Stars, UserName = x.User.FirstName}).ToList(),
                    Images =
                        x.ProductImages
                        .Select(x =>
                            new ProductImageItemDTO { Id = x.Id, Name = x.Name, ProductId = x.ProductId, ProductName = x.Product.Name })
                        .ToList()
                }).ToListAsync();
            return Ok(result);
        }
        [HttpGet("get/{id:int}")]
        public async Task<ActionResult<IEnumerable<ProductItemDTO>>> GetById(int id)
        {
            int stars = 5;
            var result = await _context.Products.Where(x => x.Id == id).Include(x => x.Category)
                .Select(x => new ProductItemDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category.Name,
                    NumberOfComments = x.Comments.Count(),
                    DateCreated = DateTime.SpecifyKind(x.DateCreated, DateTimeKind.Utc).ToString(),
                    DateUpdated = DateTime.SpecifyKind(x.DateUpdated, DateTimeKind.Utc).ToString(),
                    Comments = x.Comments.Select(x => new CommentItemDTO { Id = x.Id, Title = x.Title, Message = x.Message, ProductId = x.ProductId, UserId = x.UserId, Stars = x.Stars, UserName = x.User.FirstName }).ToList(),
                    Images =
                        x.ProductImages
                        .Select(x =>
                            new ProductImageItemDTO { Id = x.Id, Name = x.Name, ProductId = x.ProductId, ProductName = x.Product.Name })
                        .ToList()
                }).ToListAsync();
            if (result.Count > 0)
            {
                return new ObjectResult(result[0]);
            }
            else { return NotFound(); }
        }

        [HttpPost("create")]
        public async Task<ActionResult<IEnumerable<ProductCreateDTO>>> Create([FromForm] ProductCreateDTO model)
        {
            try
            {
                if (model.Name != null)
                {
                    var product = new ProductEntity
                    {
                        Name = model.Name,
                        Description = model.Description,
                        Price = model.Price,
                        CategoryId = model.CategoryId
                    };
                    _context.Add(product);
                    _context.SaveChanges();
                    // Save Images
                    foreach (IFormFile item in model.Images)
                    {
                        string img = await ImageHelper.SaveImageAsync(item, DirectoriesInProject.ProductImages);
                        var productImage = new ProductImageEntity()
                        {
                            Name = img,
                            ProductId = product.Id
                        };
                        _context.ProductImages.Add(productImage);
                    }

                    //ImageHelper.SaveImagesForProduct(model.Images, product, _iconfiguration, _context);

                    _context.SaveChanges();
                    return Ok(product);
                }
            }
            catch  { }

            return BadRequest();

        }

        [HttpPut("edit")]
        public async Task<ActionResult<IEnumerable<ProductEditDTO>>> Edit([FromForm] ProductEditDTO model)
        {
            var product = await _context.Products.SingleOrDefaultAsync(x => x.Id == model.Id);
            if (product == null)
                return NotFound();
            if (model.ImagesUpload != null)
            {
                try
                {


                    // Delete images
                    foreach (var img in await GetProductImagesByProductIdAsync(product.Id))
                    {
                        product.ProductImages.Remove(img);
                        ImageHelper.DeleteImage(img.Name, DirectoriesInProject.ProductImages);
                    }
                    //ImageHelper.DeletePrdouctImages(product, await GetProductImagesByProductIdAsync(product.Id));
                    // Save Images
                    foreach (IFormFile item in model.ImagesUpload)
                    {
                        string img = await ImageHelper.SaveImageAsync(item, DirectoriesInProject.ProductImages);
                        var productImage = new ProductImageEntity()
                        {
                            Name = img,
                            ProductId = product.Id
                        };
                        _context.ProductImages.Add(productImage);
                    }

                    //ImageHelper.SaveImagesForProduct(model.ImagesUpload, product, _iconfiguration, _context);


                }
                catch { }
           
            }

            product.Name = model.Name;
            product.DateUpdated = DateTime.UtcNow;
            product.Description = model.Description;
            product.Price = model.Price;
            product.CategoryId = model.CategoryId == 0 ? null : model.CategoryId;

            await _context.SaveChangesAsync();
            return Ok(product);
        }
        [HttpDelete("deleteProduct/{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var product = await _context.Products.SingleOrDefaultAsync(x => x.Id == id);
            if (product == null)
                return NotFound();
            // Delete images
            foreach (var img in await GetProductImagesByProductIdAsync(product.Id))
            {
                product.ProductImages.Remove(img);
                ImageHelper.DeleteImage(img.Name, DirectoriesInProject.ProductImages);
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return Ok();
        }

        Task<List<ProductImageEntity>> GetProductImagesByProductIdAsync(int productId)
        {
            var productImages = _context.ProductImages.Where(x => x.ProductId == productId).ToListAsync();
            return productImages;
        }

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
