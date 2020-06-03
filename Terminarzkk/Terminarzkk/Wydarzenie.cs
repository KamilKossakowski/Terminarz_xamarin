using SQLite;
using System.ComponentModel;


namespace Terminarzkk
{
    [Table("Wydarzenie")]
    public class Wydarzenie: INotifyPropertyChanged
    {

        private int _dzien;
        private int _miesiac;
        private int _rok;
        private string _godz;
        private string _nazwa;
        private string _gdzie;
        private string _uwagi;
        private int _id;
        private int _wyslano;
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                this._id = value;
                OnPropertyChanged(nameof(Id));

            }
        }

        public int Dzien
        {
            get => _dzien; set
            {
                _dzien = value; OnPropertyChanged(nameof(Dzien));
            }
        }
        public int Miesiac
        {
            get => _miesiac; set
            {
                _miesiac = value; OnPropertyChanged(nameof(Miesiac));
            }
        }
        public int Rok
        {
            get => _rok; set
            {
                _rok = value; OnPropertyChanged(nameof(Rok));
            }
        }
        public string Nazwa
        {
            get => _nazwa; set
            {
                _nazwa = value; OnPropertyChanged(nameof(Nazwa));
            }
        }

        public string Gdzie
        {
            get => _gdzie; set
            {
                _gdzie = value; OnPropertyChanged(nameof(Gdzie));
            }
        }
        public string Uwagi
        {
            get => _uwagi; set
            {
                _uwagi = value; OnPropertyChanged(nameof(Uwagi));
            }
        }

        public int Wyslano
        {
            get => _wyslano; set
            {
                _wyslano = value;
                OnPropertyChanged(nameof(Wyslano));
            }
        }

        public string Godz
        {
            get => _godz; set
            {
                _godz = value;
                OnPropertyChanged(nameof(Godz));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyname)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }


    }
}
