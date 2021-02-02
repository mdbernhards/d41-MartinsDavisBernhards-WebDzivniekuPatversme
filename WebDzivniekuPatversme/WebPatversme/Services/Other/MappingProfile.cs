using AutoMapper;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Models.ViewModels;

namespace WebDzivniekuPatversme.Services.Other
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