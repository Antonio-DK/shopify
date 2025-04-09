using System.Collections.ObjectModel;
using shopify.Model;

namespace shopify
{
    public static class GlobalData
    {
        public static ObservableCollection<Proizvod> SviProizvodi { get; set; } = new ObservableCollection<Proizvod>();
    }
}
