using System;
using System.ComponentModel.DataAnnotations;

namespace WebPatversme.Models.ViewModels
{
    public class NewsViewModel
    {
        [Required]
        public string Text { set; get; }

        [Required]
        public DateTime DateCreated { set; get; }

        [Required]
        public string ImagePath { set; get; }
    }
}