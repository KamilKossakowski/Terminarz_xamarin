using SQLite;
using System.ComponentModel;


namespace Terminarzkk
{
    [Table("Email")]
    public class Email : INotifyPropertyChanged
    {
        private string _nazwa;
        private string _password;
        private int _ID;

        public string Nazwa
        {
            get => _nazwa; set
            {
                _nazwa = value;
                OnPropertyChanged(nameof(Nazwa));
            }
        }
        [PrimaryKey,AutoIncrement]
        public int ID
        {
            get => _ID; set
            {
                _ID = value;
                OnPropertyChanged(nameof(ID));
            }
        }

        public string Password { get => _password; set { _password = value;
                OnPropertyChanged(nameof(Password));
            } }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyname)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

    }
}