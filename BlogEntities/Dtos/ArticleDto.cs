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
	public class ArticleDto : DtoGetBase
	{
		public Article Article { get; set; }
	}
}
