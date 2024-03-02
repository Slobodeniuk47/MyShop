using AutoMapper;
using Data.MyShop;
using Data.MyShop.Constants;
using Data.MyShop.Entities;
using Data.MyShop.Interfaces;
using Data.MyShop.Repositories;
using Infrastructure.MyShop.Helpers;
using Infrastructure.MyShop.Interfaces;
using Infrastructure.MyShop.Models.DTO.CommentDTO;
using Infrastructure.MyShop.Models.DTO.ProductDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Infrastructure.MyShop.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductImageRepository _productImageRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ICommentImageRepository _commentImageRepository;
        private readonly IMapper _mapper;
        //Зробити так щоб при видалені комменту через _commentRepository, автоматично видалялося і _commentImageRepository
        public ProductService(IProductImageRepository productImageRepository, IProductRepository productRepository, ICommentImageRepository commentImageRepository, 
            ICommentRepository commentRepository, IMapper mapper)
        {
            _productImageRepository = productImageRepository;
            _productRepository = productRepository;
            _commentImageRepository = commentImageRepository;
            _commentRepository = commentRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse> GetProductByIdAsync(int id)
        {
            var result = await _productRepository.Products.Where(x => x.Id == id).Include(x => x.Category)
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
                            new ProductImageItemDTO { Id = x.Id, Name = $"{DirectoriesInProject.Api}/{DirectoriesInProject.ProductImages}/{x.Name}", ProductId = x.ProductId, ProductName = x.Product.Name })
                        .ToList()
                }).ToListAsync();
            return new ServiceResponse
            {
                Message = "GetProduct",
                IsSuccess = true,
                Payload = result[0]
            };
        }
        public async Task<ServiceResponse> GetProductsAsync()
        {
            var result = await _productRepository.Products
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
                            new ProductImageItemDTO { Id = x.Id, Name = $"{DirectoriesInProject.Api}/{DirectoriesInProject.ProductImages}/{x.Name}", ProductId = x.ProductId, ProductName = x.Product.Name })
                        .ToList()
                }).ToListAsync();

            return new ServiceResponse
            {
                Message = "GetProducts",
                IsSuccess = true,
                Payload = result
            };
        }

        public async Task<ServiceResponse> GetProductsByCategoryId(int id)
        {
            var result = await _productRepository.Products
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
                    Comments = x.Comments.Select(x => new CommentItemDTO { Id = x.Id, Title = x.Title, Message = x.Message, ProductId = x.ProductId, UserId = x.UserId, Stars = x.Stars, UserName = x.User.FirstName }).ToList(),
                    Images =
                        x.ProductImages
                        .Select(x =>
                            new ProductImageItemDTO { Id = x.Id, Name = $"{DirectoriesInProject.Api}/{DirectoriesInProject.ProductImages}/{x.Name}", ProductId = x.ProductId, ProductName = x.Product.Name })
                        .ToList()
                }).ToListAsync();
            return new ServiceResponse
            {
                Message = "GetProducts",
                IsSuccess = true,
                Payload = result,
            };
        }
        public async Task<ServiceResponse> GetProductByNameAsync(string name)
        {
            var res = await _productRepository.GetByName(name);
            var item = _mapper.Map<ProductEntity, ProductItemDTO>(res);



            return new ServiceResponse
            {
                Message = "GetProduct",
                IsSuccess = true,
                Payload = item
            };
        }

        public async Task<ServiceResponse> CreateProductAsync(ProductCreateDTO model)
        {

            if (model.Name != null)
            {
                ProductEntity product = _mapper.Map<ProductCreateDTO, ProductEntity>(model);
                await _productRepository.Create(product);
                // Save Images
                foreach (IFormFile item in model.Images)
                {
                    string img = await ImageHelper.SaveImageAsync(item, DirectoriesInProject.ProductImages);
                    var productImage = new ProductImageEntity()
                    {
                        Name = img,
                        ProductId = product.Id
                    };
                    await _productImageRepository.Create(productImage);
                }
            }
            return new ServiceResponse
            {
                Message = "The product has been created",
                IsSuccess = true,
            };
        }
        public async Task<ServiceResponse> EditProductAsync(ProductEditDTO model)
        {
            var oldProduct = await _productRepository.GetById(model.Id);
            var product = _mapper.Map(model, oldProduct);
            product.DateUpdated = DateTime.UtcNow;

            await _productRepository.Update(product);

            if (product == null)
                return new ServiceResponse
                {
                    Message = "Product not found",
                    IsSuccess = false,
                };
            if (model.ImagesUpload != null)
            {
                    // Delete images
                    foreach (var img in await _productImageRepository.GetProductImagesByProductIdAsync(product.Id))
                    {
                        ImageHelper.DeleteImage(img.Name, DirectoriesInProject.ProductImages);
                    }
                    await _productImageRepository.RemoveProductImagesByProductIdAsync(product.Id);
                    
                    // Save Images
                    foreach (var item in model.ImagesUpload)
                    {
                        string img = await ImageHelper.SaveImageAsync(item, DirectoriesInProject.ProductImages);
                        var productImage = new ProductImageEntity()
                        {
                            Name = img,
                            ProductId = product.Id
                        };
                        await _productImageRepository.Create(productImage);
                    }
            }
            return new ServiceResponse
            {
                Message = "The product has been updated successfully",
                IsSuccess = true,
            };
        }
        public async Task<ServiceResponse> DeleteProductAsync(int id)
        {
            var product = await _productRepository.GetById(id);
            if(product == null)
            {
                return new ServiceResponse
                {
                    Message = "Product not found",
                    IsSuccess = false,
                };
            }
            // Delete images
            foreach (var img in await _productImageRepository.GetProductImagesByProductIdAsync(product.Id))
            {
                ImageHelper.DeleteImage(img.Name, DirectoriesInProject.ProductImages);
            }
            //await _commentImageRepository.Delete()
            await _productImageRepository.RemoveProductImagesByProductIdAsync(product.Id);
            await _productRepository.Delete(product.Id);
            return new ServiceResponse()
            {
                Message = "Product has been deleted",
                IsSuccess = true,
            };
        }


    }
}
