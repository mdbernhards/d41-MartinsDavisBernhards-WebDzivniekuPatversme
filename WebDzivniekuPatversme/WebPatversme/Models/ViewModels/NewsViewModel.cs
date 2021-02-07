using System;
using System.ComponentModel.DataAnnotations;

namespace WebDzivniekuPatversme.Models.ViewModels
{
    public class NewsViewModel
    {
        [Key]
        public string NewsID { set; get; }

        [Required(ErrorMessage = "Ziņu teksts ir obligāts.")]
        [Display(Name = "Teksts")]
        public string Text { set; get; }

        [Display(Name = "Izveidošanas datums")]
        public DateTime DateAdded { set; get; }

        public string ImagePath { set; get; }


        [Required(ErrorMessage = "Ziņu tituls ir obligāts.")]
        [Display(Name = "Tituls")]
        public string Title { set; get; }

        public string UserID { set; get; }
    }
}