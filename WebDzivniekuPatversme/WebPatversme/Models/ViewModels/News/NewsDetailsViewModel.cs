using System;
using System.ComponentModel.DataAnnotations;

namespace WebDzivniekuPatversme.Models.ViewModels.News
{
    public class NewsDetailsViewModel
    {
        [Key]
        public string Id { set; get; }

        [Display(Name = "Teksts")]
        public string Text { set; get; }

        [Display(Name = "Izveidošanas datums")]
        public DateTime DateAdded { set; get; }

        [Display(Name = "Tituls")]
        public string Title { set; get; }

        [Display(Name = "Autors")]
        public string UsersName { set; get; }
    }
}