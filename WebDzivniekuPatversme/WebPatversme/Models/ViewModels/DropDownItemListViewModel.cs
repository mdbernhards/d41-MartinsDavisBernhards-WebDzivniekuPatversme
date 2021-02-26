using System.Collections.Generic;

namespace WebDzivniekuPatversme.Models.ViewModels
{
    public class DropDownItemListViewModel
    {
        public List<DropDownItem> Age { get; set; }

        public List<DropDownItem> Species { get; set; }

        public List<DropDownItem> Colour { get; set; }

        public List<DropDownItem> Shelter { get; set; }

        public List<DropDownItem> Weight { get; set; }
    }
}