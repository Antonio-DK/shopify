using System.Collections.ObjectModel;
using Plugin.LocalNotification;
using shopify.Model;

namespace shopify
{
    public partial class StranicaPocetna : ContentPage
    {
        public ObservableCollection<Proizvod> Proizvodi { get; set; }
        public static ObservableCollection<KosaricaStavka> Kosarica { get; set; } = new ObservableCollection<KosaricaStavka>();

        public StranicaPocetna()
        {
            InitializeComponent();
            Proizvodi = new ObservableCollection<Proizvod>
            {
                new Proizvod { Id = 1, Naziv = "Laptop", Opis = "Visokoučinkoviti laptop s impresivnim performansama.", Cijena = 1500.00, SlikaUrl="laptop.png" },
                new Proizvod { Id = 2, Naziv = "Smartphone", Opis = "Najnoviji smartphone s vrhunskim kamerama.", Cijena = 900.00, SlikaUrl="phone.png" },
                new Proizvod { Id = 3, Naziv = "Tablet", Opis = "Praktični tablet idealan za multimediju.", Cijena = 600.00, SlikaUrl="tablet.png" },
                new Proizvod { Id = 4, Naziv = "Slusalice", Opis = "Bežične slušalice s izvrsnim zvukom.", Cijena = 120.00, SlikaUrl="slusalice.png" },
                new Proizvod { Id = 5, Naziv = "Sat", Opis = "Moderno pametno naručje s mnogo funkcija.", Cijena = 250.00, SlikaUrl="sat.png" },
                new Proizvod { Id = 6, Naziv = "Kamera", Opis = "Profesionalna kamera za vrhunski kvalitet.", Cijena = 800.00, SlikaUrl="kamera.png" },
                new Proizvod { Id = 7, Naziv = "Televizor", Opis = "4K Smart televizor s izvrsnom slikom.", Cijena = 1100.00, SlikaUrl="tv.png" },
                new Proizvod { Id = 8, Naziv = "Printer", Opis = "Brzi laserski printer za svakodnevnu upotrebu.", Cijena = 300.00, SlikaUrl="printer.png" },
                new Proizvod { Id = 9, Naziv = "Monitor", Opis = "Visokorezolucijski monitor za rad i zabavu.", Cijena = 400.00, SlikaUrl="monitor.png" },
                new Proizvod { Id = 10, Naziv = "Tipkovnica", Opis = "Bežična tipkovnica za udobno tipkanje.", Cijena = 80.00, SlikaUrl="tipkovnica.png" }
            };
            GlobalData.SviProizvodi = Proizvodi;
            BindingContext = this;
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var filtrirani = new ObservableCollection<Proizvod>();
            foreach (var proizvod in Proizvodi)
                if (proizvod.Naziv.ToLower().Contains(e.NewTextValue.ToLower()))
                    filtrirani.Add(proizvod);
            cvProizvodi.ItemsSource = filtrirani;
        }

        private async void OnSortClicked(object sender, EventArgs e)
        {
            string sortOption = await DisplayActionSheet("Sortiraj po:", "Otkaži", null, "Naziv uzlazno", "Cijena uzlazno", "Cijena silazno");
            ObservableCollection<Proizvod> sortirano = null;
            switch (sortOption)
            {
                case "Naziv uzlazno": sortirano = new ObservableCollection<Proizvod>(Proizvodi.OrderBy(p => p.Naziv)); break;
                case "Cijena uzlazno": sortirano = new ObservableCollection<Proizvod>(Proizvodi.OrderBy(p => p.Cijena)); break;
                case "Cijena silazno": sortirano = new ObservableCollection<Proizvod>(Proizvodi.OrderByDescending(p => p.Cijena)); break;
                default: return;
            }
            cvProizvodi.ItemsSource = sortirano;
        }

        private async void CvProizvodi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Proizvod odabrani)
            {
                await Navigation.PushAsync(new StranicaDetalji(odabrani, Proizvodi));
                ((CollectionView)sender).SelectedItem = null;
            }
        }

        private async void BtnKosarica_Clicked(object sender, EventArgs e) => await Navigation.PushAsync(new StranicaKosarica());
    }
}