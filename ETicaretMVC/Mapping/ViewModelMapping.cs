using AutoMapper;
using ETicaretMVC.Models;
using ETicaretMVC.ViewModels;

namespace ETicaretMVC.Mapping
{
    public class ViewModelMapping : Profile
    {
        public ViewModelMapping()
        {
            CreateMap<Product,ProductViewModel>().ReverseMap();
            CreateMap<Product, SearchViewModel>().ReverseMap();
            CreateMap<ContactMessage, ContactMessageViewModel>().ReverseMap();
            CreateMap<Comment, CommentViewModel>().ReverseMap();
            CreateMap<Orders, OrderUserViewModel>().ReverseMap();
            CreateMap<Basket, BasketViewModel>().ReverseMap();
            CreateMap<Favorite, FavoriteViewModel>().ReverseMap();
            CreateMap<Orders, OrderViewModel>().ReverseMap();
            CreateMap<Bill, BillViewModel>().ReverseMap();
        }
    }
}
