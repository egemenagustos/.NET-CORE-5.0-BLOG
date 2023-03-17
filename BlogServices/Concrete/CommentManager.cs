using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BlogData.Abstract;
using BlogEntities.Concrete;
using BlogEntities.Dtos;
using BlogServices.Abstract;
using BlogServices.Utilities;
using BlogShared.Utilities.Results.Abstract;
using BlogShared.Utilities.Results.ComplexTypes;
using BlogShared.Utilities.Results.Concrete;

namespace BlogServices.Concrete
{
    public class CommentManager: ManagerBase, ICommentService
    {

        public CommentManager(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork,mapper)
        {
        }

        public async Task<IDataResult<CommentDto>> GetAsync(int commentId)
        {
            var comment = await UnitOfWork.Comments.GetAsync(c => c.Id == commentId);
            if (comment != null)
            {
                return new DataResult<CommentDto>(ResultStates.Success, new CommentDto
                {
                    Comment = comment,
                });
            }
            return new DataResult<CommentDto>(ResultStates.Error, Messages.Comment.NotFound(isPlural: false), new CommentDto
            {
                Comment = null,
            });
        }

        public async Task<IDataResult<CommentUpdateDto>> GetCommentUpdateDtoAsync(int commentId)
        {
            var result = await UnitOfWork.Comments.AnyAsync(c => c.Id == commentId);
            if (result)
            {
                var comment = await UnitOfWork.Comments.GetAsync(c => c.Id == commentId);
                var commentUpdateDto = Mapper.Map<CommentUpdateDto>(comment);
                return new DataResult<CommentUpdateDto>(ResultStates.Success, commentUpdateDto);
            }
            else
            {
                return new DataResult<CommentUpdateDto>(ResultStates.Error, Messages.Comment.NotFound(isPlural: false), null);
            }
        }

        public async Task<IDataResult<CommentListDto>> GetAllAsync()
        {
            var comments = await UnitOfWork.Comments.GetAllAsync(null, x=>x.Article);
            if (comments.Count > -1)
            {
                return new DataResult<CommentListDto>(ResultStates.Success, new CommentListDto
                {
                    Comments = comments,
                });
            }
            return new DataResult<CommentListDto>(ResultStates.Error, Messages.Comment.NotFound(isPlural: true), new CommentListDto
            {
                Comments = null,
            });
        }

        public async Task<IDataResult<CommentListDto>> GetAllByDeletedAsync()
        {
            var comments = await UnitOfWork.Comments.GetAllAsync(c=>c.IsDeleted, x => x.Article);
            if (comments.Count > -1)
            {
                return new DataResult<CommentListDto>(ResultStates.Success, new CommentListDto
                {
                    Comments = comments,
                });
            }
            return new DataResult<CommentListDto>(ResultStates.Error, Messages.Comment.NotFound(isPlural: true), new CommentListDto
            {
                Comments = null,
            });
        }

        public async Task<IDataResult<CommentListDto>> GetAllByNonDeletedAsync()
        {
            var comments = await UnitOfWork.Comments.GetAllAsync(c => !c.IsDeleted, x => x.Article);
            if (comments.Count > -1)
            {
                return new DataResult<CommentListDto>(ResultStates.Success, new CommentListDto
                {
                    Comments = comments,
                });
            }
            return new DataResult<CommentListDto>(ResultStates.Error, Messages.Comment.NotFound(isPlural: true), new CommentListDto
            {
                Comments = null,
            });
        }

        public async Task<IDataResult<CommentListDto>> GetAllByNonDeletedAndActiveAsync()
        {
            var comments = await UnitOfWork.Comments.GetAllAsync(c => !c.IsDeleted&&c.IsActive, x => x.Article);
            if (comments.Count > -1)
            {
                return new DataResult<CommentListDto>(ResultStates.Success, new CommentListDto
                {
                    Comments = comments,
                });
            }
            return new DataResult<CommentListDto>(ResultStates.Error, Messages.Comment.NotFound(isPlural: true), new CommentListDto
            {
                Comments = null,
            });
        }

        public async Task<IDataResult<CommentDto>> AddAsync(CommentAddDto commentAddDto)
        {
            var article = await UnitOfWork.Articles.GetAsync(x=>x.Id == commentAddDto.ArticleId);
            if (article == null)
            {
                return new DataResult<CommentDto>(ResultStates.Error, Messages.Article.NotFound(isPlural: false), null);
            }
            var comment = Mapper.Map<Comment>(commentAddDto);
            var addedComment = await UnitOfWork.Comments.AddAsync(comment);
            article.CommentCount += 1;
            await UnitOfWork.Articles.UpdateAsync(article);
            await UnitOfWork.SaveAsync();
            return new DataResult<CommentDto>(ResultStates.Success, Messages.Comment.Add(commentAddDto.CreatedByName), new CommentDto
            {
                Comment = addedComment,
            });
        }

        public async Task<IDataResult<CommentDto>> UpdateAsync(CommentUpdateDto commentUpdateDto, string modifiedByName)
        {
            var oldComment = await UnitOfWork.Comments.GetAsync(c => c.Id == commentUpdateDto.Id);
            var comment = Mapper.Map<CommentUpdateDto, Comment>(commentUpdateDto, oldComment);
            comment.ModifiedByName = modifiedByName;
            var updatedComment = await UnitOfWork.Comments.UpdateAsync(comment);
            updatedComment.Article = await UnitOfWork.Articles.GetAsync(x => x.Id == updatedComment.ArticleId);
            await UnitOfWork.SaveAsync();
            return new DataResult<CommentDto>(ResultStates.Success, Messages.Comment.Update(comment.CreatedByName), new CommentDto
            {
                Comment = updatedComment,
            });
        }

        public async Task<IDataResult<CommentDto>> DeleteAsync(int commentId, string modifiedByName)
        {
            var comment = await UnitOfWork.Comments.GetAsync(c => c.Id == commentId, x=>x.Article);
            if (comment != null)
            {
                var article = comment.Article;
                comment.IsDeleted = true;
                comment.ModifiedByName = modifiedByName;
                comment.ModifiedDate = DateTime.Now;
                var deletedComment = await UnitOfWork.Comments.UpdateAsync(comment);
                article.CommentCount -= 1;
                await UnitOfWork.Articles.UpdateAsync(article);
                await UnitOfWork.SaveAsync();
                return new DataResult<CommentDto>(ResultStates.Success, Messages.Comment.Delete(deletedComment.CreatedByName), new CommentDto
                {
                    Comment = deletedComment,
                });
            }
            return new DataResult<CommentDto>(ResultStates.Error, Messages.Comment.NotFound(isPlural: false), new CommentDto
            {
                Comment = null,
            });
        }

        public async Task<IResult> HardDeleteAsync(int commentId)
        {
            var comment = await UnitOfWork.Comments.GetAsync(c => c.Id == commentId, x=>x.Article);
            if (comment != null)
            {
                if (comment.IsDeleted)
                {
                    await UnitOfWork.Comments.DeleteAsync(comment);
                    await UnitOfWork.SaveAsync();
                    return new Result(ResultStates.Success, Messages.Comment.HardDelete(comment.CreatedByName));
                }
                var article = comment.Article;
                await UnitOfWork.Comments.DeleteAsync(comment);
                article.CommentCount = await UnitOfWork.Comments.CountAsync(x=>x.ArticleId == article.Id && !x.IsDeleted);
                await UnitOfWork.Articles.UpdateAsync(article);
                await UnitOfWork.SaveAsync();
                return new Result(ResultStates.Success, Messages.Comment.HardDelete(comment.CreatedByName));
            }
            return new Result(ResultStates.Error, Messages.Comment.NotFound(isPlural: false));
        }

        public async Task<IDataResult<int>> CountAsync()
        {
            var commentsCount = await UnitOfWork.Comments.CountAsync();
            if (commentsCount > -1)
            {
                return new DataResult<int>(ResultStates.Success, commentsCount);
            }
            else
            {
                return new DataResult<int>(ResultStates.Error, $"Beklenmeyen bir hata ile karşılaşıldı.", -1);
            }
        }

        public async Task<IDataResult<int>> CountByNonDeletedAsync()
        {
            var commentsCount = await UnitOfWork.Comments.CountAsync(c=>!c.IsDeleted);
            if (commentsCount > -1)
            {
                return new DataResult<int>(ResultStates.Success, commentsCount);
            }
            else
            {
                return new DataResult<int>(ResultStates.Error, $"Beklenmeyen bir hata ile karşılaşıldı.", -1);
            }
        }

        public async Task<IDataResult<CommentDto>> ApproveAsync(int commentId, string modifiedByName)
        {
            var comment = await UnitOfWork.Comments.GetAsync(x=>x.Id == commentId, x=>x.Article);
            if(comment != null)
            {
                var article = comment.Article;
                comment.IsActive = true;
                comment.ModifiedByName = modifiedByName;
                comment.ModifiedDate= DateTime.Now;
                var updatedComment = await UnitOfWork.Comments.UpdateAsync(comment);
                article.CommentCount = await UnitOfWork.Comments.CountAsync(x => x.ArticleId == article.Id && !x.IsDeleted);
                await UnitOfWork.Articles.UpdateAsync(article);
                await UnitOfWork.SaveAsync();
                return new DataResult<CommentDto>(ResultStates.Success, Messages.Comment.Approve(commentId), new CommentDto
                {
                    Comment = updatedComment
                });
            }
            return new DataResult<CommentDto>(ResultStates.Error, Messages.Comment.NotFound(isPlural: false), null);
        }

        public async Task<IDataResult<CommentDto>> UndoDeleteAsync(int commentId, string modifiedByName)
        {
            var comment = await UnitOfWork.Comments.GetAsync(c => c.Id == commentId, x=>x.Article);
            if (comment != null)
            {
                var article = comment.Article;
                comment.IsDeleted = false;
                comment.IsActive = true;
                comment.ModifiedByName = modifiedByName;
                comment.ModifiedDate = DateTime.Now;
                var deletedComment = await UnitOfWork.Comments.UpdateAsync(comment);
                article.CommentCount += 1;
                await UnitOfWork.Articles.UpdateAsync(article);
                await UnitOfWork.SaveAsync();
                return new DataResult<CommentDto>(ResultStates.Success, Messages.Comment.UndoDelete(deletedComment.CreatedByName), new CommentDto
                {
                    Comment = deletedComment,
                });
            }
            return new DataResult<CommentDto>(ResultStates.Error, Messages.Comment.NotFound(isPlural: false), new CommentDto
            {
                Comment = null,
            });
        }
    }
}
