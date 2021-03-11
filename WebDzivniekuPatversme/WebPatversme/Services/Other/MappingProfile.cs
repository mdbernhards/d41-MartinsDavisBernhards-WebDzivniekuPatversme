using AutoMapper;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Models.ViewModels.Animal;
using WebDzivniekuPatversme.Models.ViewModels.News;
using WebDzivniekuPatversme.Models.ViewModels.Shelter;

namespace WebDzivniekuPatversme.Services.Other
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Animal, AnimalViewModel>();
            CreateMap<AnimalViewModel, Animal>();

            CreateMap<News, NewsViewModel>();
            CreateMap<NewsViewModel, News>();

            CreateMap<Shelter, ShelterViewModel>();
            CreateMap<ShelterViewModel, Shelter>();
        }
    }
}