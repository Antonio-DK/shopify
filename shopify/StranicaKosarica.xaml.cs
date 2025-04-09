using System.Collections.ObjectModel;
using shopify.Model;
using Plugin.LocalNotification;

namespace shopify
{
    public partial class StranicaKosarica : ContentPage
    {
        public ObservableCollection<KosaricaStavka> Kosarica { get; set; }
        public StranicaKosarica()
        {
            InitializeComponent();
            Kosarica = StranicaPocetna.Kosarica;
            BindingContext = this;
            UpdateTotal();
        }

        private void UpdateTotal()
        {
            double total = Kosarica.Sum(item => item.Kolicina * item.Proizvod.Cijena);
            lblTotal.Text = "Ukupno: " + total.ToString("N2") + " €";
        }

        private void BtnPovecaj_Clicked(object sender, EventArgs e)
        {
            if (((Button)sender).CommandParameter is KosaricaStavka stavka)
            {
                stavka.Kolicina++;
                UpdateTotal();
            }
        }

        private void BtnSmanji_Clicked(object sender, EventArgs e)
        {
            if (((Button)sender).CommandParameter is KosaricaStavka stavka && stavka.Kolicina > 1)
            {
                stavka.Kolicina--;
                UpdateTotal();
            }
        }

        private async void BtnObrisi_Clicked(object sender, EventArgs e)
        {
            if (((Button)sender).CommandParameter is KosaricaStavka stavka)
            {
                bool potvrda = await DisplayAlert("Potvrda", "Jeste li sigurni da želite obrisati proizvod iz košarice?", "Da", "Ne");
                if (potvrda)
                {
                    Kosarica.Remove(stavka);
                    UpdateTotal();
                }
            }
        }

        private async void BtnDovrsiKupnju_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Kupnja", "Kupnja je dovršena!", "U redu");
            Kosarica.Clear();
            UpdateTotal();
            LocalNotificationCenter.Current.Cancel(1);
        }
    }
}
