using System.Collections.Generic;

namespace WebDzivniekuPatversme.Models.ViewModels
{
    public class SheltersListViewModel
    {
        public List<SheltersViewModel> Shelters { get; set; }

        public SheltersViewModel Filter { get; set; }
    }
}