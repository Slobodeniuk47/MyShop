using Data.MyShop.Entities;
using Data.MyShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.MyShop.Repositories
{
    public class CommentImageRepository : GenericRepository<CommentImageEntity>, ICommentImageRepository
    {
        public CommentImageRepository(ApplicationDbContext context) : base(context)
        {
        }
        //public IQueryable<CommentImageEntity> CommentImages => GetAll();
    }
}
