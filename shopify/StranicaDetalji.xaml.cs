using System.Collections.ObjectModel;
using System.Linq;
using shopify.Model;
using Plugin.LocalNotification;
using Microsoft.Maui.Controls;

namespace shopify
{
    public partial class StranicaDetalji : ContentPage
    {
        private Proizvod trenutniProizvod;
        public ObservableCollection<Proizvod> PovezaniProizvodi { get; set; }
        public StranicaDetalji(Proizvod proizvod, ObservableCollection<Proizvod> sviProizvodi)
        {
            InitializeComponent();

            trenutniProizvod = proizvod;
            lblNaziv.Text = proizvod.Naziv;
            lblOpis.Text = proizvod.Opis;
            lblCijena.Text = proizvod.Cijena.ToString("N2") + " €";
            imgProizvod.Source = proizvod.SlikaUrl;
            PovezaniProizvodi = new ObservableCollection<Proizvod>(
                sviProizvodi.Where(p => p.Id != trenutniProizvod.Id)
                           .OrderBy(x => Guid.NewGuid())
                           .Take(3)
            );
            BindingContext = this;
        }

        private void BtnDodajUKosaricu_Clicked(object sender, EventArgs e)
        {
            var stavka = StranicaPocetna.Kosarica.FirstOrDefault(s => s.Proizvod.Id == trenutniProizvod.Id);
            if (stavka != null)
                stavka.Kolicina++;
            else
                StranicaPocetna.Kosarica.Add(new KosaricaStavka { Proizvod = trenutniProizvod, Kolicina = 1 });

            DisplayAlert("Košarica", "Proizvod dodan u košaricu!", "U redu");
        }

        private async void CvPovezani_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Proizvod odabrani)
            {
                await Navigation.PushAsync(new StranicaDetalji(odabrani, GlobalData.SviProizvodi));
                ((CollectionView)sender).SelectedItem = null;
            }
        }
        private async void OnHomeButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}
