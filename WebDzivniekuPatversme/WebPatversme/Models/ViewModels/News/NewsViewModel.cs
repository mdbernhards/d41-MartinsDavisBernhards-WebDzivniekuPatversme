using System;
using System.ComponentModel.DataAnnotations;

namespace WebDzivniekuPatversme.Models.ViewModels.News
{
    public class NewsViewModel
    {
        [Key]
        public string Id { set; get; }

        [Display(Name = "Teksts")]
        public string Text { set; get; }

        [Display(Name = "Izveidošanas datums")]
        public DateTime DateAdded { set; get; }

        public string ImagePath { set; get; }

        [Display(Name = "Tituls")]
        public string Title { set; get; }

        public string UserId { set; get; }
    }
}