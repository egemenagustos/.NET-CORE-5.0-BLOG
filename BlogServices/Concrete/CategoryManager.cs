using AutoMapper;
using BlogData.Abstract;
using BlogEntities.Concrete;
using BlogEntities.Dtos;
using BlogServices.Abstract;
using BlogServices.Utilities;
using BlogShared.Utilities.Results.Abstract;
using BlogShared.Utilities.Results.ComplexTypes;
using BlogShared.Utilities.Results.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogServices.Concrete
{
    public class CategoryManager : ManagerBase, ICategoryService
    {

        public CategoryManager(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        /// <summary>
        /// Verilen CategoryAddDto ve CreatedByName parametlerine ait bilgiler ile yeni bir Category ekler.
        /// </summary>
        /// <param name="categoryAddDto">CategoryAddDto tipinde eklenecek kategori bilgileri</param>
        /// <param name="createdByName">string tipinde kullanıcının kullanıcı adı</param>
        /// <returns>Asenkron bir operasyon ile Task olarak bizlere ekleme işleminin sonucunu DataResult tipinde döner.</returns>
        public async Task<IDataResult<CategoryDto>> AddAsync(CategoryAddDto categoryAddDto, string createdByName)
        {
            var category = Mapper.Map<Category>(categoryAddDto);
            category.CreatedByName = createdByName;
            category.ModifiedByName = createdByName;
            var addedCategory = await UnitOfWork.Categories.AddAsync(category);
            await UnitOfWork.SaveAsync();
            return new DataResult<CategoryDto>(ResultStates.Success, Messages.Category.Add(addedCategory.Name), new CategoryDto
            {
                Category = addedCategory,
                ResultStates = ResultStates.Success,
                Message = Messages.Category.Add(addedCategory.Name)
            });
        }

        public async Task<IDataResult<int>> CountAsync()
        {
            var categoriesCount = await UnitOfWork.Categories.CountAsync();
            if (categoriesCount > -1)
            {
                return new DataResult<int>(ResultStates.Success, categoriesCount);
            }
            else
            {
                return new DataResult<int>(ResultStates.Error, $"Beklenmeyen bir hata ile karşılaşıldı", -1);
            }
        }

        public async Task<IDataResult<int>> CountByNonDeletedAsync()
        {
            var categoriesCount = await UnitOfWork.Categories.CountAsync(x => !x.IsDeleted);
            if (categoriesCount > -1)
            {
                return new DataResult<int>(ResultStates.Success, categoriesCount);
            }
            else
            {
                return new DataResult<int>(ResultStates.Error, $"Beklenmeyen bir hata ile karşılaşıldı", -1);
            }
        }

        public async Task<IDataResult<CategoryDto>> DeleteAsync(int categoryId, string modifiedByName)
        {
            var category = await UnitOfWork.Categories.GetAsync(x => x.Id == categoryId);
            if (category != null)
            {
                category.IsDeleted = true;
                category.IsActive = false;
                category.ModifiedByName = modifiedByName;
                category.ModifiedDate = DateTime.Now;
                var deletedCategory = await UnitOfWork.Categories.UpdateAsync(category);
                await UnitOfWork.SaveAsync();
                return new DataResult<CategoryDto>(ResultStates.Success, Messages.Category.Delete(deletedCategory.Name), new CategoryDto
                {
                    Category = deletedCategory,
                    ResultStates = ResultStates.Success,
                    Message = Messages.Category.Delete(deletedCategory.Name)
                });
            }
            return new DataResult<CategoryDto>(ResultStates.Error, Messages.Category.NotFound(isPlural: false), new CategoryDto
            {
                Category = null,
                ResultStates = ResultStates.Error,
                Message = Messages.Category.NotFound(isPlural: false)
            });
        }

        public async Task<IDataResult<CategoryDto>> GetAsync(int categoryId)
        {
            var query = UnitOfWork.Categories.GetAsQueryable();
            query.Include(x=>x.Articles).ThenInclude(x=>x.Comments);
            var category = await UnitOfWork.Categories.GetAsync(x => x.Id == categoryId);
            if (category != null)
            {
                return new DataResult<CategoryDto>(ResultStates.Success, new CategoryDto
                {
                    Category = category,
                    ResultStates = ResultStates.Success,
                });
            }
            return new DataResult<CategoryDto>(ResultStates.Error, Messages.Category.NotFound(isPlural: false), new CategoryDto
            {
                Category = null,
                ResultStates = ResultStates.Error,
                Message = Messages.Category.NotFound(isPlural: false)
            });
        }

        public async Task<IDataResult<CategoryListDto>> GetAllAsync()
        {
            var categories = await UnitOfWork.Categories.GetAllAsync(null);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStates.Success, new CategoryListDto
                {
                    Categories = categories,
                    ResultStates = ResultStates.Success,
                });
            }
            return new DataResult<CategoryListDto>(ResultStates.Error, Messages.Category.NotFound(isPlural: true), new CategoryListDto
            {
                Categories = null,
                ResultStates = ResultStates.Error,
                Message = Messages.Category.NotFound(isPlural: true)
            });
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAsync()
        {
            var categories = await UnitOfWork.Categories.GetAllAsync(x => !x.IsDeleted);
            if (categories.Count > -1)

            {
                return new DataResult<CategoryListDto>(ResultStates.Success, new CategoryListDto
                {
                    Categories = categories,
                    ResultStates = ResultStates.Success,
                });
            }
            return new DataResult<CategoryListDto>(ResultStates.Error, Messages.Category.NotFound(isPlural: true), new CategoryListDto
            {
                Categories = null,
                ResultStates = ResultStates.Error,
                Message = Messages.Category.NotFound(isPlural: true)
            });
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAndActiveAsync()
        {
            var categories = await UnitOfWork.Categories.GetAllAsync(x => !x.IsDeleted && x.IsActive);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStates.Success, new CategoryListDto
                {
                    Categories = categories,
                    ResultStates = ResultStates.Success,
                });
            }
            return new DataResult<CategoryListDto>(ResultStates.Error, Messages.Category.NotFound(isPlural: true), null);
        }

        /// <summary>
        /// Verilen ID Parametresine ait kategorinin CategoryUpdateDto temsilini geriye döner.
        /// </summary>
        /// <param name="categoryId">0'dan büyük integer bir ID Değeri</param>
        /// <returns>Asenkron bir operasyon ile Task olarak işlem sonucunu DataResult tipinde geriye döner.</returns>
        public async Task<IDataResult<CategoryUpdateDto>> GetCategoryUpdateDtoAsync(int categoryId)
        {
            var result = await UnitOfWork.Categories.AnyAsync(x => x.Id == categoryId);
            if (result)
            {
                var category = await UnitOfWork.Categories.GetAsync(x => x.Id == categoryId);
                var categoryUpdateDto = Mapper.Map<CategoryUpdateDto>(category);
                return new DataResult<CategoryUpdateDto>(ResultStates.Success, categoryUpdateDto);
            }
            return new DataResult<CategoryUpdateDto>(ResultStates.Error, Messages.Category.NotFound(isPlural: false), null);
        }

        public async Task<IResult> HardDeleteAsync(int categoryId)
        {
            var category = await UnitOfWork.Categories.GetAsync(x => x.Id == categoryId);
            if (category != null)
            {
                await UnitOfWork.Categories.DeleteAsync(category);
                await UnitOfWork.SaveAsync();
                return new Result(ResultStates.Success, Messages.Category.HardDelete(category.Name));
            }
            return new Result(ResultStates.Error, Messages.Category.NotFound(isPlural: false));
        }

        public async Task<IDataResult<CategoryDto>> UpdateAsync(CategoryUpdateDto categoryUpdateDto, string modifiedByName)
        {
            var oldCatageory = await UnitOfWork.Categories.GetAsync(x => x.Id == categoryUpdateDto.Id);
            var category = Mapper.Map<CategoryUpdateDto, Category>(categoryUpdateDto, oldCatageory);
            category.ModifiedByName = modifiedByName;
            var updatedCategory = await UnitOfWork.Categories.UpdateAsync(category);
            await UnitOfWork.SaveAsync();
            return new DataResult<CategoryDto>(ResultStates.Success, Messages.Category.Update(updatedCategory.Name), new CategoryDto
            {
                Category = updatedCategory,
                ResultStates = ResultStates.Success,
                Message = Messages.Category.Update(updatedCategory.Name)
            });
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByDeletedAsync()
        {
            var categories = await UnitOfWork.Categories.GetAllAsync(x => x.IsDeleted);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStates.Success, new CategoryListDto
                {
                    Categories = categories,
                    ResultStates = ResultStates.Success,
                });
            }
            return new DataResult<CategoryListDto>(ResultStates.Error, Messages.Category.NotFound(isPlural: true), null);
        }

        public async Task<IDataResult<CategoryDto>> UndoDeleteAsync(int categoryId, string modifiedByName)
        {
            var category = await UnitOfWork.Categories.GetAsync(x => x.Id == categoryId);
            if (category != null)
            {
                category.IsDeleted = false;
                category.IsActive = true;
                category.ModifiedByName = modifiedByName;
                category.ModifiedDate = DateTime.Now;
                var deletedCategory = await UnitOfWork.Categories.UpdateAsync(category);
                await UnitOfWork.SaveAsync();
                return new DataResult<CategoryDto>(ResultStates.Success, Messages.Category.UndoDelete(deletedCategory.Name), new CategoryDto
                {
                    Category = deletedCategory,
                    ResultStates = ResultStates.Success,
                    Message = Messages.Category.UndoDelete(deletedCategory.Name)
                });
            }
            return new DataResult<CategoryDto>(ResultStates.Error, Messages.Category.NotFound(isPlural: false), new CategoryDto
            {
                Category = null,
                ResultStates = ResultStates.Error,
                Message = Messages.Category.NotFound(isPlural: false)
            });
        }
    }
}
