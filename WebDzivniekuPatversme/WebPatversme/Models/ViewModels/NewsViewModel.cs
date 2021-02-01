using System;
using System.ComponentModel.DataAnnotations;

namespace WebPatversme.Models.ViewModels
{
    public class NewsViewModel
    {
        [Required(ErrorMessage = "Ziņu teksts ir obligāts.")]
        [Display(Name = "Teksts")]
        public string Text { set; get; }

        public DateTime DateCreated { set; get; }

        public string ImagePath { set; get; }
    }
}