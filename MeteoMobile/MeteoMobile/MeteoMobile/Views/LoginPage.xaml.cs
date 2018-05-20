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
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            //TODO: create a waiting page until token come
            await PutTaskDelay();
            if (!string.IsNullOrEmpty(Settings.AccessToken))
                await Navigation.PushModalAsync(new HomePage());
            else
                await DisplayAlert("Echec", "Veuillez Recommencer", "Ok");
        }
        async Task PutTaskDelay()
        {
            await Task.Delay(5000);
        }
    }
}