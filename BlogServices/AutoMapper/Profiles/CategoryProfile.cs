using AutoMapper;
using BlogEntities.Concrete;
using BlogEntities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogServices.AutoMapper.Profiles
{
	public class CategoryProfile : Profile
	{
		public CategoryProfile()
		{
			CreateMap<CategoryAddDto, Category>().ForMember(x=>x.CreatedDate, x=>x.MapFrom(x=>DateTime.Now));
			CreateMap<CategoryUpdateDto, Category>().ForMember(x=>x.CreatedDate, x=>x.MapFrom(x=>DateTime.Now));
			CreateMap<Category,CategoryUpdateDto>();
		}
	}
}
