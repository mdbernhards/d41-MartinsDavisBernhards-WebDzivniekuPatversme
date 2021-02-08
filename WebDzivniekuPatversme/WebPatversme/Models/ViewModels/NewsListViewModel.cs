using System.Collections.Generic;

namespace WebDzivniekuPatversme.Models.ViewModels
{
    public class NewsListViewModel
    {
        public List<NewsViewModel> News { get; set; }

        public NewsViewModel Filter { get; set; }
    }
}