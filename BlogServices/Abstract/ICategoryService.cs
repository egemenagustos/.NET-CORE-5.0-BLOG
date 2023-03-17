﻿using BlogEntities.Concrete;
using BlogEntities.Dtos;
using BlogShared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogServices.Abstract
{
	public interface ICategoryService
	{
		Task<IDataResult<CategoryDto>> GetAsync(int categoryId);

        // <summary>
        /// Verilen ID Parametresine ait kategorinin CategoryUpdateDto temsilini geriye döner.
        /// </summary>
        /// <param name="categoryId">0'dan büyük integer bir ID Değeri</param>
        /// <returns>Asenkron bir operasyon ile Task olarak işlem sonucunu DataResult tipinde geriye döner.</returns>
        Task<IDataResult<CategoryUpdateDto>> GetCategoryUpdateDtoAsync(int categoryId);

		Task<IDataResult<CategoryListDto>> GetAllAsync();

		Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAsync();

		Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAndActiveAsync();

        Task<IDataResult<CategoryListDto>> GetAllByDeletedAsync();

        /// <summary>
        /// Verilen CategoryAddDto ve CreatedByName parametlerine ait bilgiler ile yeni bir Category ekler.
        /// </summary>
        /// <param name="categoryAddDto">CategoryAddDto tipinde eklenecek kategori bilgileri</param>
        /// <param name="createdByName">string tipinde kullanıcının kullanıcı adı</param>
        /// <returns>Asenkron bir operasyon ile Task olarak bizlere ekleme işleminin sonucunu DataResult tipinde döner.</returns>
        Task<IDataResult<CategoryDto>> AddAsync(CategoryAddDto categoryAddDto, string createdByName);

		Task<IDataResult<CategoryDto>> UpdateAsync(CategoryUpdateDto categoryUpdateDto, string modifiedByName);

        Task<IDataResult<CategoryDto>> DeleteAsync(int categoryId, string modifiedByName);

        Task<IDataResult<CategoryDto>> UndoDeleteAsync(int categoryId, string modifiedByName);

        Task<IResult> HardDeleteAsync(int categoryId);

		Task<IDataResult<int>> CountAsync();

		Task<IDataResult<int>> CountByNonDeletedAsync();
	}
}