using MeteoMobile.Helpers;
using MeteoMobile.Services;
using MeteoMobile.ViewModels;
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
    public partial class SignUpPage : ContentPage
    {

        public SignUpPage()
        {
            InitializeComponent();
            Title = "Enregistrer un utilisateur";
        }


        private async void Button_Clicked(object sender, EventArgs e)
        {
            await PutTaskDelay(5000);
            var vm = (SignUpViewModel)this.BindingContext;
            DisplayConfirmAlert(vm.IsSuccess);
            await App.Current.MainPage.Navigation.PopModalAsync();
        }

        public void DisplayConfirmAlert(bool result)
        {
            if (result)
            {
                DisplayAlert("Succès", "Votre utilisateur a était enregistré avec succès", "Ok");
            }
            else DisplayAlert("Echec", "Votre utilisateur n'a pas était enregistré", "Ok");
        }
        async Task PutTaskDelay(int delay)
        {
            await Task.Delay(delay);
        }
    }
}