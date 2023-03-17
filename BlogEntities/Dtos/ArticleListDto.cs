using BlogEntities.Concrete;
using BlogShared.Entities.Abstract;
using BlogShared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEntities.Dtos
{
	public class ArticleListDto : DtoGetBase
	{
		public IList<Article> Articles { get; set; }

		public int? CategoryId { get; set; }
	}
}
