using System.ComponentModel;


namespace shopify.Model
{
    public class KosaricaStavka : INotifyPropertyChanged
    {
        private int _kolicina;
        public Proizvod Proizvod { get; set; }

        public int Kolicina
        {
            get => _kolicina;
            set
            {
                _kolicina = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Kolicina)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
