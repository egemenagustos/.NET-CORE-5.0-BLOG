using AutoMapper;
using BlogData.Abstract;
using BlogEntities.ComplexType;
using BlogEntities.Concrete;
using BlogEntities.Dtos;
using BlogServices.Abstract;
using BlogServices.Utilities;
using BlogShared.Entities.Concrete;
using BlogShared.Utilities.Results.Abstract;
using BlogShared.Utilities.Results.ComplexTypes;
using BlogShared.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static BlogServices.Utilities.Messages;

namespace BlogServices.Concrete
{
    public class ArticleManager : ManagerBase, IArticleService
    {
        private readonly UserManager<User> _userManager;

        public ArticleManager(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> usermanager) : base(unitOfWork, mapper)
        {
            _userManager = usermanager;
        }

        public async Task<IResult> AddAsync(ArticleAddDto articleAddDto, string createdByName, int userId)
        {
            var article = Mapper.Map<BlogEntities.Concrete.Article>(articleAddDto);
            article.CreatedByName = createdByName;
            article.ModifiedByName = createdByName;
            article.UserId = userId;
            await UnitOfWork.Articles.AddAsync(article);
            await UnitOfWork.SaveAsync();
            return new Result(ResultStates.Success, $"{articleAddDto.Title} başlıklı makale başarıyla eklenmiştir.");
        }

        public async Task<IDataResult<int>> CountAsync()
        {
            var articlesCount = await UnitOfWork.Articles.CountAsync();
            if (articlesCount > -1)
            {
                return new DataResult<int>(ResultStates.Success, articlesCount);
            }
            else
            {
                return new DataResult<int>(ResultStates.Error, $"Beklenmeyen bir hata ile karşılaşıldı", -1);
            }
        }

        public async Task<IDataResult<int>> CountByNonDeletedAsync()
        {
            var articlesCount = await UnitOfWork.Articles.CountAsync(x => !x.IsDeleted);
            if (articlesCount > -1)
            {
                return new DataResult<int>(ResultStates.Success, articlesCount);
            }
            else
            {
                return new DataResult<int>(ResultStates.Error, $"Beklenmeyen bir hata ile karşılaşıldı", -1);
            }
        }

        public async Task<IResult> DeleteAsync(int articleId, string modifiedByName)
        {
            var result = await UnitOfWork.Articles.AnyAsync(x => x.Id == articleId);
            if (result)
            {
                var article = await UnitOfWork.Articles.GetAsync(x => x.Id == articleId);
                article.IsDeleted = true;
                article.IsActive = false;
                article.ModifiedByName = modifiedByName;
                article.ModifiedDate = DateTime.Now;
                await UnitOfWork.Articles.UpdateAsync(article);
                await UnitOfWork.SaveAsync();
                return new Result(ResultStates.Success, $"{article.Title} başlıklı makale başarıyla silinmiştir.");
            }
            return new Result(ResultStates.Error, Messages.Article.NotFound(isPlural: false));
        }

        public async Task<IDataResult<ArticleDto>> GetAsync(int articleId)
        {
            var article = await UnitOfWork.Articles.GetAsync(x => x.Id == articleId, x => x.User, x => x.Category);
            if (article != null)
            {
                article.Comments = await UnitOfWork.Comments.GetAllAsync(x => x.ArticleId == article.Id && !x.IsDeleted && x.IsActive);
                return new DataResult<ArticleDto>(ResultStates.Success, new ArticleDto
                {
                    Article = article,
                    ResultStates = ResultStates.Success,
                });
            }
            return new DataResult<ArticleDto>(ResultStates.Error, Messages.Article.NotFound(isPlural: false), null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAllAsync()
        {
            var articles = await UnitOfWork.Articles.GetAllAsync(null, x => x.User, x => x.Category);
            if (articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(ResultStates.Success, new ArticleListDto
                {
                    Articles = articles,
                    ResultStates = ResultStates.Success,
                });
            }
            return new DataResult<ArticleListDto>(ResultStates.Error, Messages.Article.NotFound(isPlural: true), null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByCategoryAsync(int categoryId)
        {
            var result = await UnitOfWork.Categories.AnyAsync(x => x.Id == categoryId);
            if (result)
            {
                var articles = await UnitOfWork.Articles.GetAllAsync(x => x.CategoryId == categoryId && !x.IsDeleted && x.IsActive, x => x.User, x => x.Category);
                if (articles.Count > -1)
                {
                    return new DataResult<ArticleListDto>(ResultStates.Success, new ArticleListDto
                    {
                        Articles = articles,
                        ResultStates = ResultStates.Success,
                    });
                }
                return new DataResult<ArticleListDto>(ResultStates.Error, Messages.Article.NotFound(isPlural: true), null);
            }
            return new DataResult<ArticleListDto>(ResultStates.Error, "Böyle bir kategori bulunamadı", null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByNonDeletedAsync()
        {
            var articles = await UnitOfWork.Articles.GetAllAsync(x => !x.IsDeleted, x => x.User, x => x.Category);
            if (articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(ResultStates.Success, new ArticleListDto
                {
                    Articles = articles,
                    ResultStates = ResultStates.Success,
                });
            }
            return new DataResult<ArticleListDto>(ResultStates.Error, Messages.Article.NotFound(isPlural: true), null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByNonDeletedActiveAsync()
        {
            var articles = await UnitOfWork.Articles.GetAllAsync(x => !x.IsDeleted && x.IsActive, x => x.User, x => x.Category);
            if (articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(ResultStates.Success, new ArticleListDto
                {
                    Articles = articles,
                    ResultStates = ResultStates.Success,
                });
            }
            return new DataResult<ArticleListDto>(ResultStates.Error, Messages.Article.NotFound(isPlural: true), null);
        }

        public async Task<IResult> HardDeleteAsync(int articleId)
        {
            var result = await UnitOfWork.Articles.AnyAsync(x => x.Id == articleId);
            if (result)
            {
                var article = await UnitOfWork.Articles.GetAsync(x => x.Id == articleId);
                await UnitOfWork.Articles.DeleteAsync(article);
                await UnitOfWork.SaveAsync();
                return new Result(ResultStates.Success, $"{article.Title} başlıklı makale başarıyla veritabanından silinmiştir.");
            }
            return new Result(ResultStates.Error, Messages.Article.NotFound(isPlural: false));
        }

        public async Task<IResult> UpdateAsync(ArticleUpdateDto articleUpdateDto, string modifiedByName)
        {
            var oldArticle = await UnitOfWork.Articles.GetAsync(x => x.Id == articleUpdateDto.Id);
            var article = Mapper.Map<ArticleUpdateDto, BlogEntities.Concrete.Article>(articleUpdateDto, oldArticle);
            article.ModifiedByName = modifiedByName;
            await UnitOfWork.Articles.UpdateAsync(article);
            await UnitOfWork.SaveAsync();
            return new Result(ResultStates.Success, $"{articleUpdateDto.Title} başlıklı makale güncellenmiştir.");
        }

        public async Task<IDataResult<ArticleUpdateDto>> GetArticleUpdateAsync(int articleId)
        {
            var result = await UnitOfWork.Articles.AnyAsync(x => x.Id == articleId);
            if (result)
            {
                var article = await UnitOfWork.Articles.GetAsync(x => x.Id == articleId);
                var articleUpdateDto = Mapper.Map<ArticleUpdateDto>(article);
                return new DataResult<ArticleUpdateDto>(ResultStates.Success, articleUpdateDto);
            }
            return new DataResult<ArticleUpdateDto>(ResultStates.Error, Messages.Category.NotFound(isPlural: false), null);
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByDeletedAsync()
        {
            var articles = await UnitOfWork.Articles.GetAllAsync(x => x.IsDeleted, x => x.User, x => x.Category);
            if (articles.Count > -1)
            {
                return new DataResult<ArticleListDto>(ResultStates.Success, new ArticleListDto
                {
                    Articles = articles,
                    ResultStates = ResultStates.Success,
                });
            }
            return new DataResult<ArticleListDto>(ResultStates.Error, Messages.Article.NotFound(isPlural: true), null);
        }

        public async Task<IResult> UndoDeleteAsync(int articleId, string modifiedByName)
        {
            var result = await UnitOfWork.Articles.AnyAsync(x => x.Id == articleId);
            if (result)
            {
                var article = await UnitOfWork.Articles.GetAsync(x => x.Id == articleId);
                article.IsDeleted = false;
                article.IsActive = true;
                article.ModifiedByName = modifiedByName;
                article.ModifiedDate = DateTime.Now;
                await UnitOfWork.Articles.UpdateAsync(article);
                await UnitOfWork.SaveAsync();
                return new Result(ResultStates.Success, Messages.Article.UndoDelete(article.Title));
            }
            return new Result(ResultStates.Error, Messages.Article.NotFound(isPlural: false));
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByViewCountAsync(bool isAscending, int? takeSize)
        {
            var articles = await UnitOfWork.Articles.GetAllAsync(x => x.IsActive && !x.IsDeleted, x => x.User);
            /* Ascending : Artan / Descending : Azalan */
            var sortedArticles = isAscending ? articles.OrderBy(x => x.ViewsCount) : articles.OrderByDescending(x => x.ViewsCount);
            return new DataResult<ArticleListDto>(ResultStates.Success, new ArticleListDto
            {
                Articles = takeSize == null ? sortedArticles.ToList() : sortedArticles.Take(takeSize.Value).ToList()
            });
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByPagingAsync(int? categoryId, int currentPage = 1, int pageSize = 5, bool isAscending = false)
        {
            pageSize = pageSize > 20 ? 20 : pageSize;
            var articles = categoryId == null ? await UnitOfWork.Articles.GetAllAsync(x => x.IsActive && !x.IsDeleted, x => x.Category, x => x.User) : await UnitOfWork.Articles.GetAllAsync(x => x.CategoryId == categoryId && x.IsActive && !x.IsDeleted, x => x.Category, x => x.User);
            var sortedArticles = isAscending ? articles.OrderBy(x => x.Date).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList() : articles.OrderByDescending(x => x.Date).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            return new DataResult<ArticleListDto>(ResultStates.Success, new ArticleListDto
            {
                Articles = sortedArticles,
                CategoryId = categoryId == null ? null : categoryId.Value,
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalCount = articles.Count,
                IsAscending = isAscending
            });
        }

        public async Task<IDataResult<ArticleListDto>> SearchAsync(string keyword, int currentPage = 1, int pageSize = 5, bool isAscending = false)
        {
            pageSize = pageSize > 20 ? 20 : pageSize;
            if (string.IsNullOrEmpty(keyword))
            {
                var articles = await UnitOfWork.Articles.GetAllAsync(x => x.IsActive && !x.IsDeleted, x => x.Category, x => x.User);
                var sortedArticles = isAscending ? articles.OrderBy(x => x.Date).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList() : articles.OrderByDescending(x => x.Date).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                return new DataResult<ArticleListDto>(ResultStates.Success, new ArticleListDto
                {
                    Articles = sortedArticles,
                    CurrentPage = currentPage,
                    PageSize = pageSize,
                    TotalCount = articles.Count,
                    IsAscending = isAscending
                });
            }
            var searchArticles = await UnitOfWork.Articles.SearchAsync(new List<Expression<Func<BlogEntities.Concrete.Article, bool>>>
            {
                (a)=>a.Title.Contains(keyword),
                (a)=>a.Category.Name.Contains(keyword),
                (a)=>a.SeoDescription.Contains(keyword),
                (a)=>a.SeoTags.Contains(keyword)
            }, x => x.Category, x => x.User);
            var searchedAndSortedArticles = isAscending ? searchArticles.OrderBy(x => x.Date).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList() : searchArticles.OrderByDescending(x => x.Date).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            return new DataResult<ArticleListDto>(ResultStates.Success, new ArticleListDto
            {
                Articles = searchedAndSortedArticles,
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalCount = searchArticles.Count,
                IsAscending = isAscending
            });
        }

        public async Task<IResult> IncreaseViewCountAsync(int articleId)
        {
            var article = await UnitOfWork.Articles.GetAsync(x => x.Id == articleId);
            if (article == null)
            {
                return new Result(ResultStates.Error, Messages.Article.NotFound(isPlural: false));
            }
            article.ViewsCount += 1;
            await UnitOfWork.Articles.UpdateAsync(article);
            await UnitOfWork.SaveAsync();
            return new Result(ResultStates.Success, Messages.Article.IncreasViewCount(article.Title));
        }

        public async Task<IDataResult<ArticleListDto>> GetAllByUserIdOnFilter(int userId, FilterBy filterBy, OrderBy orderBy, bool isAscending, int takeSize, int categoryId, DateTime startAt, DateTime endAt, int minViewCount, int maxViewCount, int minCommentCount, int maxCommentCount)
        {
            var anyUser = await _userManager.Users.AnyAsync(x => x.Id == userId);
            if (!anyUser)
            {
                return new DataResult<ArticleListDto>(ResultStates.Error, $"{userId} numaralı kullanıcı bulunamadı", null);
            }
            var userArticles = await UnitOfWork.Articles.GetAllAsync(x => x.UserId == userId && x.IsActive && !x.IsDeleted);
            List<BlogEntities.Concrete.Article> sortedArticles = new List<BlogEntities.Concrete.Article>();
            switch (filterBy)
            {
                case FilterBy.Category:
                    switch (orderBy)
                    {
                        case OrderBy.Date:
                            sortedArticles = isAscending ? userArticles.Where(x => x.CategoryId == categoryId).Take(takeSize).OrderBy(x => x.Date).ToList() : userArticles.Where(x => x.CategoryId == categoryId).Take(takeSize).OrderByDescending(x => x.Date).ToList();
                            break;
                        case OrderBy.ViewCount:
                            sortedArticles = isAscending ? userArticles.Where(x => x.CategoryId == categoryId).Take(takeSize).OrderBy(x => x.ViewsCount).ToList() : userArticles.Where(x => x.CategoryId == categoryId).Take(takeSize).OrderByDescending(x => x.ViewsCount).ToList();
                            break;
                        case OrderBy.CommentCount:
                            sortedArticles = isAscending ? userArticles.Where(x => x.CategoryId == categoryId).Take(takeSize).OrderBy(x => x.CommentCount).ToList() : userArticles.Where(x => x.CategoryId == categoryId).Take(takeSize).OrderByDescending(x => x.CommentCount).ToList();
                            break;
                    }
                    break;
                case FilterBy.Date:
                    switch (orderBy)
                    {
                        case OrderBy.Date:
                            sortedArticles = isAscending ? userArticles.Where(x => x.Date >= startAt && x.Date <= endAt).Take(takeSize).OrderBy(x => x.Date).ToList() : userArticles.Where(x => x.Date >= startAt && x.Date <= endAt).Take(takeSize).OrderByDescending(x => x.Date).ToList();
                            break;
                        case OrderBy.ViewCount:
                            sortedArticles = isAscending ? userArticles.Where(x => x.Date >= startAt && x.Date <= endAt).Take(takeSize).OrderBy(x => x.ViewsCount).ToList() : userArticles.Where(x => x.Date >= startAt && x.Date <= endAt).Take(takeSize).OrderByDescending(x => x.ViewsCount).ToList();
                            break;
                        case OrderBy.CommentCount:
                            sortedArticles = isAscending ? userArticles.Where(x => x.Date >= startAt && x.Date <= endAt).Take(takeSize).OrderBy(x => x.CommentCount).ToList() : userArticles.Where(x => x.Date >= startAt && x.Date <= endAt).Take(takeSize).OrderByDescending(x => x.CommentCount).ToList();
                            break;
                    }
                    break;
                case FilterBy.ViewCount:
                    switch (orderBy)
                    {
                        case OrderBy.Date:
                            sortedArticles = isAscending ? userArticles.Where(x => x.ViewsCount >= minViewCount && x.ViewsCount <= maxViewCount).Take(takeSize).OrderBy(x => x.Date).ToList() : userArticles.Where(x => x.ViewsCount >= minViewCount && x.ViewsCount <= maxViewCount).Take(takeSize).OrderByDescending(x => x.Date).ToList();
                            break;
                        case OrderBy.ViewCount:
                            sortedArticles = isAscending ? userArticles.Where(x => x.ViewsCount >= minViewCount && x.ViewsCount <= maxViewCount).Take(takeSize).OrderBy(x => x.ViewsCount).ToList() : userArticles.Where(x => x.ViewsCount >= minViewCount && x.ViewsCount <= maxViewCount).Take(takeSize).OrderByDescending(x => x.ViewsCount).ToList();
                            break;
                        case OrderBy.CommentCount:
                            sortedArticles = isAscending ? userArticles.Where(x => x.ViewsCount >= minViewCount && x.ViewsCount <= maxViewCount).Take(takeSize).OrderBy(x => x.CommentCount).ToList() : userArticles.Where(x => x.ViewsCount >= minViewCount && x.ViewsCount <= maxViewCount).Take(takeSize).OrderByDescending(x => x.CommentCount).ToList();
                            break;
                    }
                    break;
                case FilterBy.CommentCount:
                    switch (orderBy)
                    {
                        case OrderBy.Date:
                            sortedArticles = isAscending ? userArticles.Where(x => x.CommentCount >= minCommentCount && x.CommentCount <= maxCommentCount).Take(takeSize).OrderBy(x => x.Date).ToList() : userArticles.Where(x => x.CommentCount >= minCommentCount && x.CommentCount <= maxCommentCount).Take(takeSize).OrderByDescending(x => x.Date).ToList();
                            break;
                        case OrderBy.ViewCount:
                            sortedArticles = isAscending ? userArticles.Where(x => x.CommentCount >= minCommentCount && x.CommentCount <= maxCommentCount).Take(takeSize).OrderBy(x => x.ViewsCount).ToList() : userArticles.Where(x => x.CommentCount >= minCommentCount && x.CommentCount <= maxCommentCount).Take(takeSize).OrderByDescending(x => x.ViewsCount).ToList();
                            break;
                        case OrderBy.CommentCount:
                            sortedArticles = isAscending ? userArticles.Where(x => x.CommentCount >= minCommentCount && x.CommentCount <= maxCommentCount).Take(takeSize).OrderBy(x => x.CommentCount).ToList() : userArticles.Where(x => x.CommentCount >= minCommentCount && x.CommentCount <= maxCommentCount).Take(takeSize).OrderByDescending(x => x.CommentCount).ToList();
                            break;
                    }
                    break;
            }
            return new DataResult<ArticleListDto>(ResultStates.Success, new ArticleListDto
            {
                Articles = sortedArticles
            });
        }

        public async Task<IDataResult<ArticleDto>> GetByIdAsync(int articleId, bool includeCategory, bool includeComments, bool includeUser)
        {
            List<Expression<Func<BlogEntities.Concrete.Article, bool>>> predicates = new List<Expression<Func<BlogEntities.Concrete.Article, bool>>>();
            List<Expression<Func<BlogEntities.Concrete.Article, object>>> includes = new List<Expression<Func<BlogEntities.Concrete.Article, object>>>();

            if (includeCategory) includes.Add(x => x.Category);
            if (includeComments) includes.Add(x => x.Comments);
            if (includeComments) includes.Add(x => x.User);
            predicates.Add(x => x.Id == articleId);
            var article = await UnitOfWork.Articles.GetAsyncV2(predicates, includes);
            if (article == null)
            {
                return new DataResult<ArticleDto>(ResultStates.Warning, Messages.General.ValidationError(), null, new List<ValidationError>
                {
                    new ValidationError
                    {
                        PropertyName = "articleId",
                        Message = Messages.Article.NotFoundById(articleId)
                    }
                });
            }
            return new DataResult<ArticleDto>(ResultStates.Success, new ArticleDto
            {
                Article = article
            });
        }

        public async Task<IDataResult<ArticleListDto>> GetAllAsyncV2(int? categoryId, int? userId, bool? isActive, bool? isDeleted, int currentPage,
        int pageSize, OrderByGeneral orderBy, bool isAscending, bool includeCategory, bool includeComments, bool includeUser)
        {
            List<Expression<Func<BlogEntities.Concrete.Article, bool>>> predicates = new List<Expression<Func<BlogEntities.Concrete.Article, bool>>>();
            List<Expression<Func<BlogEntities.Concrete.Article, object>>> includes = new List<Expression<Func<BlogEntities.Concrete.Article, object>>>();

            if (categoryId.HasValue)
            {
                if (!await UnitOfWork.Categories.AnyAsync(x => x.Id == categoryId.Value))
                {
                    return new DataResult<ArticleListDto>(ResultStates.Warning, Messages.General.ValidationError(), null, new List<ValidationError>
                {
                    new ValidationError
                    {
                        PropertyName = "categoryId",
                        Message = Messages.Category.NotFoundById(categoryId.Value)
                    }
                });
                }
                predicates.Add(x => x.CategoryId == categoryId.Value);
            }

            if (userId.HasValue)
            {
                if (!await _userManager.Users.AnyAsync(x => x.Id == userId.Value))
                {
                    return new DataResult<ArticleListDto>(ResultStates.Warning, Messages.General.ValidationError(), null, new List<ValidationError>
                {
                    new ValidationError
                    {
                        PropertyName = "userId",
                        Message = Messages.Users.NotFoundById(userId.Value)
                    }
                });
                }
                predicates.Add(x => x.UserId == userId.Value);
            }

            if (isActive.HasValue) predicates.Add(x => x.IsActive == isActive.Value);
            if (isDeleted.HasValue) predicates.Add(x => x.IsDeleted == isDeleted.Value);

            if (includeCategory) includes.Add(x => x.Category);
            if (includeComments) includes.Add(x => x.Comments);
            if (includeComments) includes.Add(x => x.User);

            var articles = await UnitOfWork.Articles.GetAllAsyncV2(predicates, includes);

            IOrderedEnumerable<BlogEntities.Concrete.Article> sortedArticles;

            switch (orderBy)
            {
                case OrderByGeneral.Id:
                    sortedArticles = isAscending ? articles.OrderBy(x => x.Id) : articles.OrderByDescending(x => x.Id);
                    break;

                case OrderByGeneral.Az:
                    sortedArticles = isAscending ? articles.OrderBy(x => x.Title) : articles.OrderByDescending(x => x.Title);
                    break;

                default:
                    sortedArticles = isAscending ? articles.OrderBy(x => x.CreatedDate) : articles.OrderByDescending(x => x.CreatedDate);
                    break;
            }

            return new DataResult<ArticleListDto>(ResultStates.Success, new ArticleListDto
            {
                Articles = sortedArticles.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList(),
                CategoryId = categoryId.HasValue ? categoryId.Value : null,
                CurrentPage = currentPage,
                PageSize = pageSize,
                IsAscending = isAscending,
                TotalCount = articles.Count,
                ResultStates = ResultStates.Success,
            });
        }
    }
}
