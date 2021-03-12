using AutoMapper;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Models.ViewModels.Animal;
using WebDzivniekuPatversme.Models.ViewModels.News;
using WebDzivniekuPatversme.Models.ViewModels.Shelters;

namespace WebDzivniekuPatversme.Services.Other
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Animal, AnimalViewModel>();
            CreateMap<AnimalViewModel, Animal>();

            CreateMap<Animal, AnimalCreateViewModel>();
            CreateMap<AnimalCreateViewModel, Animal>();

            CreateMap<Animal, AnimalEditViewModel>();
            CreateMap<AnimalEditViewModel, Animal>();

            CreateMap<Animal, AnimalDetailsViewModel>();
            CreateMap<AnimalDetailsViewModel, Animal>();

            CreateMap<News, NewsViewModel>();
            CreateMap<NewsViewModel, News>();

            CreateMap<News, NewsCreateViewModel>();
            CreateMap<NewsCreateViewModel, News>();

            CreateMap<News, NewsEditViewModel>();
            CreateMap<NewsEditViewModel, News>();

            CreateMap<News, NewsDetailsViewModel>();
            CreateMap<NewsDetailsViewModel, News>();

            CreateMap<Shelter, ShelterViewModel>();
            CreateMap<ShelterViewModel, Shelter>();

            CreateMap<Shelter, ShelterCreateViewModel>();
            CreateMap<ShelterCreateViewModel, Shelter>();

            CreateMap<Shelter, ShelterEditViewModel>();
            CreateMap<ShelterEditViewModel, Shelter>();

            CreateMap<Shelter, ShelterDetailsViewModel>();
            CreateMap<ShelterDetailsViewModel, Shelter>();
        }
    }
}