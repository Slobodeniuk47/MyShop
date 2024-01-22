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
        //private readonly IImageService _imageService;
        private readonly UserManager<UserEntity> _userManager;
        private readonly ICommentImageService _commentImageService;
        private readonly IMapper _mapper;

        public CommentService(ApplicationDbContext context, ICommentRepository commentRepository, UserManager<UserEntity> userManager, IMapper mapper, ICommentImageService commentImageService)
        {
            _context = context;
            _commentRepository = commentRepository;
            _userManager = userManager;
            _mapper = mapper;
            //_imageService = imageService;
            _commentImageService = commentImageService;
        }
        public async Task<IQueryable<CommentItemDTO>> GetCommentsByProductIdAsync(int id)
        {
            //List<CommentEntity> comments = _commentRepository.GetAll().Where(comment => comment.ProductId == id).ToList();
            //var comments_vms = _mapper.Map<List<CommentEntity>, List<CommentItemDTO>>(comments);
            //return comments_vms;

            var result = _commentRepository.GetAll().Where(comment => comment.ProductId == id).Select(x => new CommentItemDTO
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
            });

            //return new ServiceResponse
            //{
            //    IsSuccess = true,
            //    Payload = result
            //};
            return result;
        }


        public async Task<List<CommentEntity>> GetAllAsync()
        {
            return _commentRepository.GetAll().ToList();
        }

        Task<List<CommentImageEntity>> GetCommentImagesByCommentIdAsync(int commentId)
        {
            var commentImages = _context.CommentImages.Where(x => x.CommentId == commentId).ToListAsync();
            return commentImages;
        }


        public async Task<ServiceResponse> CreateCommentAsync(CommentCreateDTO model)
        {
            CommentEntity comment = _mapper.Map<CommentCreateDTO, CommentEntity>(model);

            await _commentRepository.Create(comment);

            if(model.Images != null)
            {
                // Image upload
                foreach (var img in model.Images)
                {
                    var imgTemplate = img;
                    var imgFileName = await ImageHelper.SaveImageAsync(imgTemplate, DirectoriesInProject.CommentImages);

                    CommentImageEntity new_img_to_upload = new CommentImageEntity { Name = imgFileName, CommentId = comment.Id };


                    await _commentImageService.CreateCommentImageAsync(new_img_to_upload);
                }
            }

            if (comment != null)
            {
                return new ServiceResponse
                {
                    IsSuccess = true,
                };
            }
            return null;

        }
        public async Task<ServiceResponse> EditCommentAsync(CommentEditDTO model)
        {
            CommentEntity comment = _mapper.Map<CommentEditDTO, CommentEntity>(model);

            await _commentRepository.Update(comment);

            if (model.Images != null)
            {
                // Delete images
                foreach (var img in await GetCommentImagesByCommentIdAsync(comment.Id))
                {
                    comment.CommentImages.Remove(img);
                    ImageHelper.DeleteImage(img.Name, DirectoriesInProject.CommentImages);
                }
                // Image upload
                foreach (var img in model.Images)
                {
                    var imgTemplate = img;
                    var imgFileName = await ImageHelper.SaveImageAsync(imgTemplate, DirectoriesInProject.CommentImages);

                    CommentImageEntity new_img_to_upload = new CommentImageEntity { Name = imgFileName, CommentId = comment.Id };

                    await _commentImageService.CreateCommentImageAsync(new_img_to_upload);
                }
            }

            if (comment != null)
            {
                return new ServiceResponse
                {
                    IsSuccess = true,
                };
            }
            return null;
        }

        
        
        public async Task<ServiceResponse> DeleteCommentAsync(int id)
        {

            var comment = await _context.Comment.SingleOrDefaultAsync(x => x.Id == id);
            if (comment == null)
            {
                return new ServiceResponse()
                {
                    Message = "Uploaded category is not correct, uploaded is closed",
                    IsSuccess = false,
                };
            }
            // Delete images
            foreach (var img in await GetCommentImagesByCommentIdAsync(comment.Id))
            {
                    comment.CommentImages.Remove(img);
                ImageHelper.DeleteImage(img.Name, DirectoriesInProject.CommentImages);
            }
            //_context.Comment.Remove(comment);
            //await _context.SaveChangesAsync();
            _commentRepository.Delete(comment);
            return new ServiceResponse()
            {
                Message = "Comment has been deleted",
                IsSuccess = true,
            };
        }
        
    }
}
