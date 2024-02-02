using AutoMapper;
using Data.MyShop;
using Data.MyShop.Constants;
using Data.MyShop.Entities;
using Data.MyShop.Entities.Identity;
using Data.MyShop.Interfaces;
using Data.MyShop.Repositories;
using Infrastructure.MyShop.Helpers;
using Infrastructure.MyShop.Interfaces;
using Infrastructure.MyShop.Models.DTO.AccountDTO;
using Infrastructure.MyShop.Models.DTO.CommentDTO;
using Infrastructure.MyShop.Models.DTO.ProductDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MyShop.Services
{
    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICommentRepository _commentRepository;        
        private readonly ICommentImageRepository _commentImageRepository;
        private readonly IMapper _mapper;

        public CommentService(ApplicationDbContext context, ICommentRepository commentRepository, IMapper mapper, ICommentImageRepository commentImageRepository)
        {
            _context = context;
            _commentRepository = commentRepository;
            _mapper = mapper;
            _commentImageRepository = commentImageRepository;
        }
        public async Task<ServiceResponse> GetCommentsByProductIdAsync(int id)
        {
            var result = await _commentRepository.GetAll().Where(comment => comment.ProductId == id).Select(x => new CommentItemDTO
            {
                Id = x.Id,
                Title = x.Title,
                Message = x.Message,
                Stars = x.Stars,
                DateCreated = DateTime.SpecifyKind(x.DateCreated, DateTimeKind.Utc).ToString(),
                DateUpdated = DateTime.SpecifyKind(x.DateUpdated, DateTimeKind.Utc).ToString(),
                Likes = x.Likes,
                Dislikes = x.Dislikes,
                UserId = x.UserId,
                UserName = x.User.FirstName,
                ProductId = x.ProductId,
                Images = x.CommentImages.Select(x => new CommentImageItemDTO { Id = x.Id, Name = x.Name, CommentId = x.CommentId, CommentName = x.Comment.Title }).ToList(),
                User = new UserItemDTO { Id = x.UserId, Firstname = x.User.FirstName, Lastname = x.User.LastName, Email = x.User.Email, phoneNumber = x.User.PhoneNumber, Image = x.User.Image }
            }).ToListAsync();

            return new ServiceResponse
            {
                IsSuccess = true,
                Payload = result
            };
        }


        public async Task<ServiceResponse> GetAllAsync()
        {
            var result = await _commentRepository.GetAll().ToListAsync();
            return new ServiceResponse
            {
                IsSuccess = true,
                Payload = result
            };
        }

        public async Task<ServiceResponse> CreateCommentAsync(CommentCreateDTO model)
        {
            CommentEntity comment = _mapper.Map<CommentCreateDTO, CommentEntity>(model);

            await _commentRepository.Create(comment);

            if(model.Images != null)
            {
                //Save images
                foreach (IFormFile img in model.Images)
                {
                    var imgFileName = await ImageHelper.SaveImageAsync(img, DirectoriesInProject.CommentImages);

                    CommentImageEntity new_img_to_upload = new CommentImageEntity { Name = imgFileName, CommentId = comment.Id };
                    await _commentImageRepository.Create(new_img_to_upload);
                }
            }

            return new ServiceResponse
            {
                Message = "The comment has been created",
                IsSuccess = true,
            };
        }
        public async Task<ServiceResponse> EditCommentAsync(CommentEditDTO model)
        {
            CommentEntity comment = _mapper.Map<CommentEditDTO, CommentEntity>(model);
            comment.DateUpdated = DateTime.UtcNow;
            if (comment != null)
            {
                return new ServiceResponse
                {
                    Message = "Comment not found",
                    IsSuccess = true,
                };
            }

            await _commentRepository.Update(comment);

            if (model.Images != null)
            {
                // Delete old images
                foreach (var img in await _commentImageRepository.GetCommentImagesByCommentIdAsync(comment.Id))
                {
                    ImageHelper.DeleteImage(img.Name, DirectoriesInProject.CommentImages);
                }
                await _commentImageRepository.RemoveCommentImagesByCommentIdAsync(comment.Id);

                //Save new images
                foreach (var img in model.Images)
                {
                    var imgFileName = await ImageHelper.SaveImageAsync(img, DirectoriesInProject.CommentImages);

                    CommentImageEntity new_img_to_upload = new CommentImageEntity 
                    { 
                        Name = imgFileName, 
                        CommentId = comment.Id 
                    };
                    await _commentImageRepository.Create(new_img_to_upload);
                }
            }
            return new ServiceResponse
            {
                Message = "The comment has been updated successfully",
                IsSuccess = true,
            };
        }

        
        
        public async Task<ServiceResponse> DeleteCommentAsync(int id)
        {

            var comment = await _context.Comment.SingleOrDefaultAsync(x => x.Id == id);
            if (comment == null)
            {
                return new ServiceResponse()
                {
                    Message = "Uploaded comment is not correct, uploaded is closed",
                    IsSuccess = false,
                };
            }
            // Delete images
            foreach (var img in await _commentImageRepository.GetCommentImagesByCommentIdAsync(comment.Id))
            {
                ImageHelper.DeleteImage(img.Name, DirectoriesInProject.CommentImages);
            }
            await _commentImageRepository.RemoveCommentImagesByCommentIdAsync(comment.Id);
            await _commentRepository.Delete(comment);
            return new ServiceResponse()
            {
                Message = "Comment has been deleted",
                IsSuccess = true,
            };
        }
        
    }
}
