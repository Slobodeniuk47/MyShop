using Data.MyShop.Entities;
using Data.MyShop.Interfaces;
using Infrastructure.MyShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.MyShop.Services
{
    public class CommentImageService : ICommentImageService
    {
        private readonly ICommentImageRepository _imageRepository;
        public CommentImageService(ICommentImageRepository commentImageRepository)
        {
            _imageRepository = commentImageRepository;
        }
    }
}
