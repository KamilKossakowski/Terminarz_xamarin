using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Terminarzkk
{
	
	public partial class Dokonajzmiany : ContentPage
	{
        
        Wydarzenie g;
        
		public Dokonajzmiany()
		{
            g =new Wydarzenie();
            
			InitializeComponent ();
		}
        public Dokonajzmiany(Wydarzenie j)
        {
            g = j;
            BindingContext = g;
            InitializeComponent();
        }

        async void OnPowHome(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MainPage());
        }
        async void OnZmienwydrzenie(object sender, EventArgs e)
        {
            bool f= true;
            int ht;
            string hty;
            ht=Convert.ToInt16(Lday.Text);
            if (ht < 32 && ht > 0) { g.Dzien = ht;  } else { await DisplayAlert("Błąd", "Ilość dni mieści się miedzy 1, a 31",null ,"ok");f = false; }
              g.Gdzie = Lgdz.Text;
              g.Godz = Lgodz.Text;
              ht = Convert.ToInt16(Lmiesic.Text);
              if (ht < 13 && ht > 0) { g.Dzien = ht; } else { await DisplayAlert("Błąd", "Ilość miesięcy mieści się miedzy 1, a 12",null , "ok");f = false; }
              g.Miesiac = Convert.ToInt16(Lmiesic.Text);
              g.Nazwa = Lnazw.Text;
              g.Rok = Convert.ToInt32(Lrok.Text);
              g.Uwagi = Luwagi.Text;
              g.Wyslano = 0;
            
            Dbwydarzenie kr =new Dbwydarzenie();
            if (f) {
                kr.ZapiszWydarzenie(g);
                await DisplayAlert("ok", "Wydarzenie zostało dodane", "ok");
                await Navigation.PushModalAsync(new MainPage());
            }
            
        }
    }
}