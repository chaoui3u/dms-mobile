using MeteoMobile.Behaviors;
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
            rolePicker.SelectedItem = rolePicker.Items.FirstOrDefault();
        }


        private async void Button_Clicked(object sender, EventArgs e)
        {
           
            if (password.TextColor != Color.Red
                 && confirmPassword.TextColor != Color.Red
                 && email.TextColor != Color.Red && !(string.IsNullOrEmpty(password.Text)
               || string.IsNullOrEmpty(confirmPassword.Text)
               || string.IsNullOrEmpty(firstName.Text)
               || string.IsNullOrEmpty(lastName.Text)
               || string.IsNullOrEmpty(email.Text)))
            {
                var vm = (SignUpViewModel)this.BindingContext;
                vm.SignUpCommand.Execute(null);
                await PutTaskDelay(5000);
                DisplayConfirmAlert(vm.IsSuccess);
                await App.Current.MainPage.Navigation.PopModalAsync();
            }else
            {
               await DisplayAlert("Erreur", "Corrigez les entrer en rouge," +
                   " et ne laissez aucun champ vide.", "Ok");
            }

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