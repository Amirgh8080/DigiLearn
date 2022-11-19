using AutoMapper;
using BlogModule.Domain;
using BlogModule.Services.DTOs.Command;
using BlogModule.Services.DTOs.Query;

namespace BlogModule;

public class MapperProfile : Profile
{
	public MapperProfile()
	{
		CreateMap<Category, BlogCategoryDto>().ReverseMap();
		CreateMap<Category, CreateCategoryCommand>().ReverseMap();
		CreateMap<Category, EditCategoryCommand>().ReverseMap();



		CreateMap<Post, CreatePostCommand>().ReverseMap();
		CreateMap<Post, EditPostCommand>().ReverseMap();
		CreateMap<Post, BlogPostDto>().ReverseMap();
	}
}
