using AutoMapper;
using WebPatversme.Models;
using WebPatversme.Models.ViewModels;

namespace WebDzivniekuPatversme.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Animals, AnimalsViewModel>();
            CreateMap<AnimalsViewModel, Animals>();

            CreateMap<News, NewsViewModel>();
            CreateMap<NewsViewModel, News>();

            CreateMap<Shelters, SheltersViewModel>();
            CreateMap<SheltersViewModel, Shelters>();
        }
    }
}