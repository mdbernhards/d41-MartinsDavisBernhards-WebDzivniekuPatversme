using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.UI.Services;
using WebDzivniekuPatversme.Models;
using WebDzivniekuPatversme.Models.ViewModels;
using WebDzivniekuPatversme.Services.Interfaces;
using WebDzivniekuPatversme.Repositories.Interfaces;
using WebDzivniekuPatversme.Models.ViewModels.Animal;

namespace WebDzivniekuPatversme.Services
{
    public class AnimalsService : IAnimalsService
    {
        private readonly IAnimalsRepository _animalsRepository;
        private readonly IShelterRepository _shelterRepository;
        private readonly IEmailSender _emailSender;

        public AnimalsService(
            IAnimalsRepository animalsRepository,
            IShelterRepository shelterRepository,
            IEmailSender emailSender)
        {
            _animalsRepository = animalsRepository;
            _shelterRepository = shelterRepository;
            _emailSender = emailSender;
        }

        public List<Shelter> GetAllShelters()
        {
            return _shelterRepository.GetAllAnimalShelters();
        }

        public List<Animal> GetAllAnimalList()
        {
            return _animalsRepository.GetAllAnimals();
        }

        public List<AnimalColour> GetAllColours()
        {
            return _animalsRepository.GetAllColours();
        }

        public List<AnimalSpecies> GetAllSpecies()
        {
            return _animalsRepository.GetAllSpecies();
        }

        public List<AnimalSpeciesType> GetAllSpeciesTypes()
        {
            return _animalsRepository.GetAllSpeciesTypes();
        }

        public List<AnimalViewModel> AddAnimalShelterNames(List<AnimalViewModel> animals)
        {
            foreach (var animal in animals)
            {
                animal.ShelterName = GetAnimalShelterName(animal.ShelterId);
            }

            return animals;
        }

        public string GetAnimalShelterName(string id)
        {
            var ShelterList = GetAllShelters();

            return ShelterList
                .Where(x => x.Id == id)
                .FirstOrDefault().Name;
        }

        public List<AnimalViewModel> FilterAndSortAnimals(List<AnimalViewModel> animals, string sortOrder, AnimalFilter filter)
        {
            animals = FilterAnimals(animals, filter);
            animals = OrderAnimals(animals, sortOrder);

            return animals;
        }

        public Animal GetAnimalById(string Id)
        {
            var AnimalList = _animalsRepository.GetAllAnimals();
            var animal = AnimalList.Where(animal => animal.Id == Id).FirstOrDefault();

            return animal;
        }

        public void DeleteAnimal(Animal animal)
        {
            _animalsRepository.DeleteAnimal(animal);
        }

        public void AddNewAnimal(Animal animal)
        {
            animal.Id = Guid.NewGuid().ToString();
            animal.DateAdded = DateTime.Now;

            _animalsRepository.CreateNewAnimal(animal);
        }

        public void EditAnimal(Animal animal)
        {
            _animalsRepository.EditAnimal(animal);
        }

        public async void SendAnimalEmail(Animal animal)
        {
            animal.Email = GetAllShelters()
                .Where(x => x.Id == animal.ShelterId)
                .Select(x => x.Email)
                .FirstOrDefault();

            if(animal.Email == null)
            {
                animal.Email = "martinsdavisbernhards@gmail.com";
            }

            await _emailSender.SendEmailAsync(animal.Email, animal.EmailTitle, animal.EmailMessage);
        }

        public AnimalFilter CreateAnimalFilter(string name, string age, string species, string speciesType, string colour, string shelter)
        {
            return new AnimalFilter
            {
                Name = name,
                Age = age,
                Species = species,
                SpeciesType = speciesType,
                Colour = colour,
                Shelter = shelter,
            };
        }

        public DropDownItemListViewModel CreateAnimalDropDownListValues(List<AnimalViewModel> animalList, AnimalFilter filter)
        {
            var listItems = GetDropDownListValueNames(animalList);
            CountDropDownListValues(animalList, listItems, filter);
            OrderDropDownListValues(listItems);

            return listItems;
        }

        private static void OrderDropDownListValues(DropDownItemListViewModel listItems)
        {
            listItems.Colour = listItems.Colour
                .OrderBy(x => x.Item)
                .ToList();

            listItems.Species = listItems.Species
                .OrderBy(x => x.Item)
                .ToList();

            listItems.SpeciesType = listItems.SpeciesType
                .OrderBy(x => x.Item)
                .ToList();

            listItems.Shelter = listItems.Shelter
                .OrderBy(x => x.Item)
                .ToList();
        }

        private static DropDownItemListViewModel GetDropDownListValueNames (List<AnimalViewModel> animalList)
        {
            var listItems = new DropDownItemListViewModel
            {
                Age = new List<DropDownItem>(),
                Species = new List<DropDownItem>(),
                SpeciesType = new List<DropDownItem>(),
                Colour = new List<DropDownItem>(),
                Shelter = new List<DropDownItem>(),
            };

            foreach (var animal in animalList.OrderByDescending(x => x.BirthDate))
            {
                if (!listItems.Age.Select(x => x.Item).Contains(animal.Age.ToString()))
                {
                    listItems.Age.Add(new DropDownItem { Item = animal.Age.ToString(), Count = 0 });
                }

                if (!listItems.Species.Select(x => x.Item).Contains(animal.Species))
                {
                    listItems.Species.Add(new DropDownItem { Item = animal.Species, Count = 0 });
                }

                if (!listItems.SpeciesType.Select(x => x.Item).Contains(animal.SpeciesType))
                {
                    listItems.SpeciesType.Add(new DropDownItem { Item = animal.SpeciesType, Count = 0 });
                }

                if (!listItems.Colour.Select(x => x.Item).Contains(animal.Colour))
                {
                    listItems.Colour.Add(new DropDownItem { Item = animal.Colour, Count = 0 });
                }

                if (!listItems.Colour.Select(x => x.Item).Contains(animal.SecondaryColour) && !animal.SecondaryColour.Contains("Nav"))
                {
                    listItems.Colour.Add(new DropDownItem { Item = animal.SecondaryColour, Count = 0 });
                }

                if (!listItems.Shelter.Select(x => x.Item).Contains(animal.ShelterName))
                {
                    listItems.Shelter.Add(new DropDownItem { Item = animal.ShelterName, Count = 0 });
                }
            }

            return listItems;
        }

        private static void CountDropDownListValues (List<AnimalViewModel> animals, DropDownItemListViewModel listItems, AnimalFilter filter)
        {
            listItems.Age = CountIndividualDropDownListValues(
                FilterAnimals( 
                    animals, 
                    new AnimalFilter { 
                        Colour = filter.Colour,
                        Species = filter.Species,
                        SpeciesType = filter.SpeciesType,
                        Shelter = filter.Shelter,
                        Name = filter.Name,
                    }).Select(x => x.Age.ToString()).ToList(),
                listItems.Age);

            var colourFilter = FilterAnimals(
                animals,
                new AnimalFilter
                {
                    Age = filter.Age,
                    Species = filter.Species,
                    SpeciesType = filter.SpeciesType,
                    Shelter = filter.Shelter,
                    Name = filter.Name,
                });

            var filteredColours = colourFilter
                .Select(x => x.Colour)
                .ToList();

            filteredColours
                .AddRange(colourFilter
                .Select(x => x.SecondaryColour)
                .ToList());

            listItems.Colour = CountIndividualDropDownListValues(
                filteredColours,
                listItems.Colour);

            listItems.Species = CountIndividualDropDownListValues(
                FilterAnimals(
                    animals, 
                    new AnimalFilter { 
                        Age = filter.Age,
                        Colour = filter.Colour,
                        SpeciesType = filter.SpeciesType,
                        Shelter = filter.Shelter,
                        Name = filter.Name,
                    }).Select(x => x.Species).ToList(),
                listItems.Species);

            listItems.SpeciesType = CountIndividualDropDownListValues(
                FilterAnimals(
                    animals,
                    new AnimalFilter
                    {
                        Age = filter.Age,
                        Colour = filter.Colour,
                        Species = filter.Species,
                        Shelter = filter.Shelter,
                        Name = filter.Name,
                    }).Select(x => x.SpeciesType).ToList(),
                listItems.SpeciesType);

            listItems.Shelter = CountIndividualDropDownListValues(
                FilterAnimals(
                    animals, 
                    new AnimalFilter { 
                        Age = filter.Age,
                        Colour = filter.Colour,
                        Species = filter.Species,
                        SpeciesType = filter.SpeciesType,
                        Name = filter.Name,
                    }).Select(x => x.ShelterName).ToList(),
                listItems.Shelter);
        }

        private static List<DropDownItem> CountIndividualDropDownListValues (List<string> filteredItem, List<DropDownItem> listItem)
        {
            foreach (var item in filteredItem)
            {
                if (listItem.Where(x => x.Item == item.ToString()).FirstOrDefault() != null) 
                {
                    listItem.Where(x => x.Item == item.ToString()).FirstOrDefault().Count++;
                }
            }

            return listItem;
        }

        private static List<AnimalViewModel> OrderAnimals(List<AnimalViewModel> animals, string sortOrder)
        {
            animals = sortOrder switch
            {
                "name_desc" => animals.OrderByDescending(s => s.Name).ToList(),
                "age" => animals.OrderBy(s => s.BirthDate).ToList(),
                "age_desc" => animals.OrderByDescending(s => s.BirthDate).ToList(),
                "species" => animals.OrderBy(s => s.Species).ToList(),
                "species_desc" => animals.OrderByDescending(s => s.Species).ToList(),
                "weight" => animals.OrderBy(s => s.Weight).ToList(),
                "weight_desc" => animals.OrderByDescending(s => s.Weight).ToList(),
                "shelter" => animals.OrderBy(s => s.ShelterName).ToList(),
                "shelter_desc" => animals.OrderByDescending(s => s.ShelterName).ToList(),
                "dateAdded" => animals.OrderBy(s => s.DateAdded).ToList(),
                "dateadded_desc" => animals.OrderByDescending(s => s.DateAdded).ToList(),
                "colour" => animals.OrderBy(s => s.Colour).ToList(),
                "colour_desc" => animals.OrderByDescending(s => s.Colour).ToList(),
                _ => animals.OrderBy(s => s.Name).ToList(),
            };

            return animals;
        }

        private static List<AnimalViewModel> FilterAnimals(List<AnimalViewModel> animals, AnimalFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.Name))
            {
                animals = animals.Where(animal => animal.Name.ToLower().Contains(filter.Name.ToLower())).ToList();
            }

            if(!string.IsNullOrEmpty(filter.Age))
            {
                animals = animals.Where(animal => animal.Age.Contains(filter.Age)).ToList();
            }

            if (!string.IsNullOrEmpty(filter.Species))
            {
                animals = animals.Where(animal => animal.Species.Contains(filter.Species)).ToList();
            }

            if (!string.IsNullOrEmpty(filter.SpeciesType))
            {
                animals = animals.Where(animal => animal.SpeciesType.Contains(filter.SpeciesType)).ToList();
            }

            if (!string.IsNullOrEmpty(filter.Colour))
            {
                var animalColours = animals
                    .Where(animal => animal.Colour
                    .Contains(filter.Colour))
                    .ToList();

                animalColours
                    .AddRange(animals
                    .Where(animal => animal.SecondaryColour
                    .Contains(filter.Colour))
                    .ToList());

                animals = animalColours
                    .Distinct()
                    .ToList();
            }

            if (!string.IsNullOrEmpty(filter.Shelter))
            {
                animals = animals.Where(animal => animal.ShelterName.Contains(filter.Shelter)).ToList();
            }

            return animals;
        }
    }
}