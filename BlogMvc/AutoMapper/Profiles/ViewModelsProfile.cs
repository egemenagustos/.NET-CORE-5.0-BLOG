using AutoMapper;
using BlogEntities.Concrete;
using BlogEntities.Dtos;
using BlogMvc.Areas.Admin.Models;

namespace BlogMvc.AutoMapper.Profiles
{
    public class ViewModelsProfile : Profile
    {
        public ViewModelsProfile()
        {
            CreateMap<ArticleAddViewModel, ArticleAddDto>();
            CreateMap<ArticleUpdateDto, ArticleUpdateViewModel>().ReverseMap();
            CreateMap<ArticleRightSideBarWidgetOptions, ArticleRightSideBarWidgetOptionsViewModel>().ReverseMap();
        }
    }
}
