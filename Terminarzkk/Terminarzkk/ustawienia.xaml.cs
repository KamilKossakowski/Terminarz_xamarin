using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Terminarzkk
{

	public partial class ustawienia : ContentPage
	{
		public ustawienia ()
        { 
            Email t = new Email();
            Dbemail em = new Dbemail(); 
            t = em.GetEmail();
            BindingContext = t;
            InitializeComponent ();
		}
        async void OnZmienEmail(object sender, EventArgs e)
        {
            string emailnew = Newemail.Text;
            if (emailnew.IndexOf("@") > 0) {
                Dbemail em = new Dbemail();
                Email t =new Email();
                t.Nazwa = emailnew;
                t.Password = newpass.Text;
                t.ID = 1;
                try
                {
                    em.Zapiszemail(t);
                }
                catch (Exception ex){ }
                await DisplayAlert("Hurra", "Adres e-mail zostal zmieniony", "ok");
                await Navigation.PushModalAsync(new MainPage());
            } else {await DisplayAlert("błędny adres e-mail", "Podaj poprawny adres e-mail", "ok"); }

        }
        async void OnPowHome(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MainPage());
        }
        }
}