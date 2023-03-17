using BlogData.Abstract;
using BlogData.Concrete.EntitiyFramework.Context;
using BlogData.Concrete.EntitiyFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogData.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context _context;

        private EfArticleRepository _efArticleRepository;

        private EfCommentRepository _commentRepository;

        private EfCategoryRepository _efCategoryRepository;


        public UnitOfWork(Context context)
        {
            _context = context;
        } 

        //?? = bir değişkenin değerinin null olduğu durumlarda alternatif olarak değer döndürebiliriz.

        public IArticleRepository Articles => _efArticleRepository ??=  new EfArticleRepository(_context);

        public ICategoryRepository Categories => _efCategoryRepository ??= new EfCategoryRepository(_context);

        public ICommentRepository Comments => _commentRepository ??= new EfCommentRepository(_context);

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
