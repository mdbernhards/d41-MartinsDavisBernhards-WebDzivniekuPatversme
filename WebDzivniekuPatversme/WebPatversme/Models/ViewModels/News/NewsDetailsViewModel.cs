using System;
using System.ComponentModel.DataAnnotations;

namespace WebDzivniekuPatversme.Models.ViewModels.News
{
    public class NewsDetailsViewModel
    {
        [Key]
        public string Id { set; get; }

        [StringLength(13000, ErrorMessage = "Teksts sasniedz savu maksimālo garumu")]
        [Required(ErrorMessage = "Ziņu teksts ir obligāts.")]
        [Display(Name = "Teksts")]
        public string Text { set; get; }

        [Display(Name = "Izveidošanas datums")]
        public DateTime DateAdded { set; get; }

        [StringLength(100, ErrorMessage = "Tituls par garu")]
        [Required(ErrorMessage = "Ziņu tituls ir obligāts.")]
        [Display(Name = "Tituls")]
        public string Title { set; get; }

        public string UserId { set; get; }
    }
}