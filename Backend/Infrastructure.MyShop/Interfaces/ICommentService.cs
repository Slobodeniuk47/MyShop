using Data.MyShop.Entities;
using Infrastructure.MyShop.Models.DTO.CommentDTO;
using Infrastructure.MyShop.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MyShop.Interfaces
{
    public interface ICommentService
    {
        Task<List<CommentEntity>> GetAllAsync();
        Task<ServiceResponse> CreateCommentAsync(CommentCreateDTO comment);
        Task<ServiceResponse> DeleteCommentAsync(int id);
        //Task<ActionResult<IEnumerable<CommentItemDTO>>> GetCommentsByProductIdAsync(int id);
        Task<ServiceResponse> EditCommentAsync(CommentEditDTO comment);
        Task<IQueryable<CommentItemDTO>> GetCommentsByProductIdAsync(int id);
        //Task<ServiceResponse> CanLeaveCommentAsync(CanLeaveCommentDTO model);
    }
}
