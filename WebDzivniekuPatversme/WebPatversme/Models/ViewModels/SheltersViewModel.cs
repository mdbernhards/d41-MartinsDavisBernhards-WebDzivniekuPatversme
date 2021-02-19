using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using WebDzivniekuPatversme.Services.Other;

namespace WebDzivniekuPatversme.Models.ViewModels
{
    public class SheltersViewModel
    {
        [Key]
        public string AnimalShelterID { set; get; }

        [StringLength(100, ErrorMessage = "Patversmes nosaukums par garu")]
        [RegularExpression(@"^[a-zA-Z'\s]*$", ErrorMessage = "Vārds var sastāvēt tikai no burtiem")]
        [Required(ErrorMessage = "Vārds ir obligāts.")]
        [Display(Name = "Vārds")]
        public string Name { set; get; }

        [Required(ErrorMessage = "Adrese ir obligāta.")]
        [Display(Name = "Adrese")]
        public string Address { set; get; }

        [StringLength(25, ErrorMessage = "Telefona numurs par garu")]
        [Phone(ErrorMessage = "Telefona numurs ievadīts nepareizi")]
        [Required(ErrorMessage = "Telefona numurs ir obligāts.")]
        [Display(Name = "Telefona numurs")]
        public string PhoneNumber { set; get; }

        [Range(1, 9999, ErrorMessage = "Kapacitāte nevar būt mazāka par 1 un lielāka par 9999.")]
        [Required(ErrorMessage = "Dzīvnieku kapacitāte ir obligāta.")]
        [Display(Name = "Dzīvnieku kapacitāte")]
        public int AnimalCapacity { set; get; }

        [Display(Name = "Izveidošanas datums")]
        public DateTime DateAdded { set; get; }

        public string ImagePath { set; get; }

        [Display(Name = "Attēls")]
        [DataType(DataType.Upload)]
        [MaxFileSizeValidation(6 * 1024 * 1024)]
        [ExtensionValidation(new string[] { ".jpg", ".png" })]
        public IFormFile Image { set; get; }
    }
}