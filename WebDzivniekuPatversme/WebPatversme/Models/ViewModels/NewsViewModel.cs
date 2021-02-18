using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace WebDzivniekuPatversme.Models.ViewModels
{
    public class NewsViewModel
    {
        [Key]
        public string NewsID { set; get; }

        [StringLength(5000, ErrorMessage = "Teksts sasniedz savu maksimālo garumu")]
        [RegularExpression(@"^[a-zA-Z'\s]*$", ErrorMessage = "Teksts var sastāvēt tikai no burtiem")]
        [Required(ErrorMessage = "Ziņu teksts ir obligāts.")]
        [Display(Name = "Teksts")]
        public string Text { set; get; }

        [Display(Name = "Izveidošanas datums")]
        public DateTime DateAdded { set; get; }

        public string ImagePath { set; get; }

        [NotMapped]
        [Display(Name = "Attēls")]
        [DataType(DataType.Upload)]
        public IFormFile Image { set; get; }

        [StringLength(100, ErrorMessage = "Tituls par garu")]
        [RegularExpression(@"^[a-zA-Z'\s]*$", ErrorMessage = "Tituls var sastāvēt tikai no burtiem")]
        [Required(ErrorMessage = "Ziņu tituls ir obligāts.")]
        [Display(Name = "Tituls")]
        public string Title { set; get; }

        public string UserID { set; get; }
    }
}