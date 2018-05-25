using MeteoMobile.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeteoMobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
            login.Text = "admin@test.local";
            pass.Text = "Password123!";
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            //TODO: create a waiting page until token come
            await PutTaskDelay(5000);
            if (string.IsNullOrEmpty(Settings.AccessToken))
                await PutTaskDelay(2000);
            if (!string.IsNullOrEmpty(Settings.AccessToken))
                await Navigation.PushModalAsync(new HomePage());
            else
                await DisplayAlert("Echec", "Veuillez Recommencer", "Ok");
        }
        async Task PutTaskDelay(int delay)
        {
            await Task.Delay(delay);
        }
    }
}