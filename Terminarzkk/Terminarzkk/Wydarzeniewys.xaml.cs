using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Terminarzkk
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Wydarzeniewys : ContentPage
	{
        Wydarzenie j;

        public Wydarzeniewys(int id)
		{
            Dbwydarzenie k = new Dbwydarzenie();
            j = k.GetWydarzenie(id);
            BindingContext = j;
            
            InitializeComponent ();
		}
        async void Onusunwyd(object sender, EventArgs e)
        {
            bool x=  await DisplayAlert("Usuwanie", "Jesteś pewny ,że chcesz usunąć to wydarzenie", "tak", "nie");
            if (x) { 
            Dbwydarzenie k = new Dbwydarzenie();
            k.UsunWydarzenie(j);
            await Navigation.PushModalAsync(new MainPage());}
        }
        async void OndokonajZmian(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Dokonajzmiany(j));
        }
        async void SEND(object sender, EventArgs e)
        {
            j.Dzien = 1;
            j.Gdzie = "DDD";
            Wysylka I=new Wysylka();
            I.Wysla_wiadomosc(j);

        }
        }
}
