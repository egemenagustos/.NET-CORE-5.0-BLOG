using BlogEntities.Concrete;
using BlogShared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEntities.Dtos
{
	public class CategoryDto : DtoGetBase
	{
		public Category Category { get; set; }
	}
}
