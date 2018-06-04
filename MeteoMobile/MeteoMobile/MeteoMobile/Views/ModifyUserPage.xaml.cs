using MeteoMobile.Helpers;
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
    public partial class ModifyUserPage : ContentPage
    {
        
        public ModifyUserPage()
        {
            InitializeComponent();
            Title = "Modifier";

        }
        protected override void OnAppearing()
        {
            if (Constants.ThisUser != null)
            {
                firstName.Text = Constants.ThisUser.FirstName;
                lastName.Text = Constants.ThisUser.LastName;
                email.Text = Constants.ThisUser.Email;
                rolePicker.SelectedItem = Constants.ThisUser.Role;
            }else
            {
                firstName.Text = Constants.MyUser.FirstName;
                lastName.Text = Constants.MyUser.LastName;
                email.Text = Constants.MyUser.Email;
                rolePicker.SelectedItem = Constants.MyUser.Role;
                password.Text = Settings.Password;
                confirmPassword.Text = Settings.Password;
                if (Constants.MyUser.Role == "Personnel")
                {
                    rolePicker.IsVisible = false;
                    roleLabel.IsVisible = false;
                }
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (password.TextColor != Color.Red
                && confirmPassword.TextColor != Color.Red
                && email.TextColor != Color.Red 
                && !( string.IsNullOrEmpty(firstName.Text)
              || string.IsNullOrEmpty(lastName.Text)
              || string.IsNullOrEmpty(email.Text)))
            {
                if (!string.IsNullOrEmpty(password.Text) && string.IsNullOrEmpty(confirmPassword.Text))
                    {
                       await DisplayAlert("erreur", "veuillez confirmer le mot de passe","Ok");
                        return;
                    }
                    
                 var vm = (ModifyUserViewModel)this.BindingContext;
                vm.ModifyUserCommand.Execute(null);
                await PutTaskDelay(5000);
                DisplayConfirmAlert(vm.IsSuccess);
                Constants.ThisUser = null;
                await App.Current.MainPage.Navigation.PopModalAsync();
                }
            else 
            {
                await DisplayAlert("Erreur", "Corrigez les champs en rouge," +
                    " et ne laissez aucun champs vide à part le mot de passe.", "Ok");
            }
        }
        public void DisplayConfirmAlert(bool result)
        {
            if (result)
            {
                DisplayAlert("Succès", "Votre utilisateur à était modifié avec succès", "Ok");
            }
            else DisplayAlert("Echec", "Votre utilisateur n'a pas était modifié", "Ok");
        }
        async Task PutTaskDelay(int delay)
        {
            await Task.Delay(delay);
        }

    }
}