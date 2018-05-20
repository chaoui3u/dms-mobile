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
    public partial class LogoutPage : ContentPage
    {
        public LogoutPage()
        {
            InitializeComponent();


        }
        protected override async void OnAppearing()
        {
            Settings.AccessToken = "";
            Application.Current.MainPage = new LoginPage();
            await DisplayAlert("Déconnection", "Vous êtes Déconnecté", "Ok");
        }
    }
}