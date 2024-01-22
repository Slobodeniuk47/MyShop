using Data.MyShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.MyShop.Interfaces
{
    public interface ICommentImageRepository : IGenericRepository<CommentImageEntity, int>
    {
        //IQueryable<CommentImageEntity> CommentImages { get; }
    }
}
