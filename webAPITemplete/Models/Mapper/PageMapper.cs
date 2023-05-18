using AutoMapper;
using WebAPITemplete.Models.DTOs.DefaultDB;
using WebAPITemplete.Models.Mapper.interfaces;
using WebAPITemplete.Models.ViewModels;

namespace WebAPITemplete.Models.Mapper
{
    public class PageMapper : IPageMapper
    {
        public MapperConfiguration ToMenuTreeViewModel()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PageDTO, MenuTreeViewModel>()
                .ForMember(dest => dest.PageID, opt => opt.MapFrom(src => src.PageID))
                .ForMember(dest => dest.PageName, opt => opt.MapFrom(src => src.PageName))
                .ForMember(dest => dest.ParentPageID, opt => opt.MapFrom(src => src.ParentPageID))
                .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level))
                .ForMember(dest => dest.SubPages, opt => opt.Ignore());
            });
        }
    }
}
