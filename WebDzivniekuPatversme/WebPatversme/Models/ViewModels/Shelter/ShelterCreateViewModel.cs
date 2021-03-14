﻿using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using WebDzivniekuPatversme.Validation;

namespace WebDzivniekuPatversme.Models.ViewModels.Shelters
{
    public class ShelterCreateViewModel
    {
        [StringLength(255, ErrorMessage = "Patversmes nosaukums par garu!")]
        [Required(ErrorMessage = "Vārds ir obligāts!")]
        [Display(Name = "Nosaukums")]
        public string Name { set; get; }

        [Required(ErrorMessage = "E-pasts ir obligāts!")]
        [StringLength(255, ErrorMessage = "E-pasts par garu!")]
        [EmailAddress]
        [Display(Name = "E-pasts")]
        public string Email { set; get; }

        [StringLength(19000, ErrorMessage = "Apraksts par garu!")]
        [Display(Name = "Apraksts")]
        public string Description { set; get; }

        [Required(ErrorMessage = "Adrese ir obligāta!")]
        [StringLength(255, ErrorMessage = "Adrese par garu!")]
        [Display(Name = "Adrese")]
        public string Address { set; get; }

        [StringLength(25, ErrorMessage = "Telefona numurs par garu!")]
        [Phone(ErrorMessage = "Telefona numurs ievadīts nepareizi!")]
        [Required(ErrorMessage = "Telefona numurs ir obligāts!")]
        [Display(Name = "Telefona numurs")]
        public string PhoneNumber { set; get; }

        [Range(1, 9999, ErrorMessage = "Kapacitāte nevar būt mazāka par 1 un lielāka par 9999!")]
        [Required(ErrorMessage = "Dzīvnieku kapacitāte ir obligāta!")]
        [Display(Name = "Dzīvnieku kapacitāte")]
        public int AnimalCapacity { set; get; }

        [Display(Name = "Attēls")]
        [DataType(DataType.Upload)]
        [MaxFileSizeValidation(6 * 1024 * 1024)]
        [ExtensionValidation(new string[] { ".jpg", ".png" })]
        public IFormFile Image { set; get; }
    }
}