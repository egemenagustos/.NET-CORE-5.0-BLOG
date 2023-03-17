using BlogData.Abstract;
using BlogEntities.Concrete;
using BlogShared.Data.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogData.Concrete.EntitiyFramework.Repositories
{
    public class EfCommentRepository : EfEntityRepositoryBase<Comment>, ICommentRepository
    {
        public EfCommentRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
