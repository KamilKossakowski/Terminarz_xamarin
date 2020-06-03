using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.StyleSheets;

namespace Terminarzkk
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        { 
            InitializeComponent();
            Dbwydarzenie k = new Dbwydarzenie();
            MainListView.ItemsSource = k.GetKazdeWydarzenia();

        }

        async void OnTapped(object sender, ItemTappedEventArgs e)
        {
            var Selected = e.Item as Wydarzenie;
            await Navigation.PushModalAsync(new Wydarzeniewys(Selected.Id));
        }

       
        async void OnUstawEmail(object sender,EventArgs e)
        {
            await Navigation.PushModalAsync(new ustawienia());
        }
        async void OndokonajZmian(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Dokonajzmiany());
        }
        
    }
}
