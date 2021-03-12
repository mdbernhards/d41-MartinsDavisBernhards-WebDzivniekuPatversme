using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using WebDzivniekuPatversme.Services.Other;

namespace WebDzivniekuPatversme.Models.ViewModels.News
{
    public class NewsCreateViewModel
    {
        [StringLength(13000, ErrorMessage = "Teksts sasniedz savu maksimālo garumu")]
        [Required(ErrorMessage = "Ziņu teksts ir obligāts.")]
        [Display(Name = "Teksts")]
        public string Text { set; get; }

        [NotMapped]
        [DataType(DataType.Upload)]
        [MaxFileSizeValidation(6 * 1024 * 1024)]
        [ExtensionValidation(new string[] { ".jpg", ".png" })]
        [Display(Name = "Attēls")]
        public IFormFile Image { set; get; }

        [StringLength(100, ErrorMessage = "Tituls par garu")]
        [Required(ErrorMessage = "Ziņu tituls ir obligāts.")]
        [Display(Name = "Tituls")]
        public string Title { set; get; }

        public string UserId { set; get; }
    }
}