using MeteoMobile.Helpers;
using MeteoMobile.Services;
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
        public LoginPage()
        {
            InitializeComponent();
            ocpImage.Source = ImageSource.FromResource("MeteoMobile.Images.ocp_logo.png");
            
            login.Text = "admin@test2.local";
            pass.Text = "Password123!";
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            //TODO: create a waiting page until token come
            indicator.IsRunning = true;
            indicator.IsVisible = true;
            login.IsEnabled = false;
            pass.IsEnabled = false;
            myButton.IsEnabled = false;
            await PutTaskDelay(5000);
            if (string.IsNullOrEmpty(Settings.AccessToken))
                await PutTaskDelay(2000);
            if (!string.IsNullOrEmpty(Settings.AccessToken))
            {
                Settings.Username = login.Text;
                Settings.Password = pass.Text;
                Constants.MyUser = await new ApiServices().GetMyUserAsync(Settings.AccessToken);
                if (Constants.MyUser == null)
                    await PutTaskDelay(2000);
                try
                {
                    await Navigation.PushModalAsync(new HomePage());
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Exception Message : " + ex.Message
                        + " Instance that caused the current message : "
                        + ex.InnerException + " Stack of Exception :"
                        + ex.StackTrace);
                    await DisplayAlert("Echec", "Veuillez Recommencer", "Ok");
                    indicator.IsRunning = false;
                    indicator.IsVisible = false;
                    login.IsEnabled = true;
                    pass.IsEnabled = true;
                    myButton.IsEnabled = true;
                }
            }
            else
            {
                await DisplayAlert("Echec", "Veuillez Recommencer", "Ok");
                indicator.IsRunning = false;
                indicator.IsVisible = false;
                login.IsEnabled = true;
                pass.IsEnabled = true;
                myButton.IsEnabled = true;
            }
        }
        async Task PutTaskDelay(int delay)
        {
            await Task.Delay(delay);
        }
    }
}