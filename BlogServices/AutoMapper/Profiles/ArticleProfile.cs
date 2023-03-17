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
	public class ArticleProfile : Profile
	{
		public ArticleProfile()
		{
			CreateMap<ArticleAddDto, Article>().ForMember(x=>x.CreatedDate, x=>x.MapFrom(x=>DateTime.Now));
			CreateMap<ArticleUpdateDto, Article>().ForMember(x=>x.CreatedDate, x=>x.MapFrom(x=>DateTime.Now));
			CreateMap<Article, ArticleUpdateDto>();
		}
	}
}
