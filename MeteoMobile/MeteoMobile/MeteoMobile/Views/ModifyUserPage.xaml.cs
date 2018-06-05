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
                if (!string.IsNullOrEmpty(password.Text) 
                    && string.IsNullOrEmpty(confirmPassword.Text) 
                    || (!string.IsNullOrEmpty(password.Text) && (password.Text != confirmPassword.Text))
                    || (!string.IsNullOrEmpty(confirmPassword.Text) && (password.Text != confirmPassword.Text)))
                    {
                       await DisplayAlert("erreur", "veuillez confirmer le mot de passe","Ok");
                        return;
                    }
                    
                 var vm = (ModifyUserViewModel)this.BindingContext;
                vm.ModifyUserCommand.Execute(null);
                firstName.IsEnabled = false;
                lastName.IsEnabled = false;
                password.IsEnabled = false;
                confirmPassword.IsEnabled = false;
                email.IsEnabled = false;
                rolePicker.IsEnabled = false;
                myButton.IsEnabled = false;
                indicator.IsRunning = true;
                indicator.IsVisible = true;
                await PutTaskDelay(6000);
                var success =DisplayConfirmAlert(vm.IsSuccess);
                if (success)
                {
                    Constants.ThisUser = null;
                    await App.Current.MainPage.Navigation.PopModalAsync();
                }
            }
            else 
            {
                await DisplayAlert("Erreur", "Corrigez les champs en rouge," +
                    " et ne laissez aucun champs vide à part le mot de passe.", "Ok");
            }
        }
        public bool DisplayConfirmAlert(bool result)
        {
            if (result)
            {
                DisplayAlert("Succès", "Votre utilisateur à était modifié avec succès", "Ok");
                return true;
            }
            else
            {
                DisplayAlert("Echec", "Votre utilisateur n'a pas était modifié", "Ok");
                firstName.IsEnabled = true;
                lastName.IsEnabled = true;
                password.IsEnabled = true;
                confirmPassword.IsEnabled = true;
                email.IsEnabled = true;
                rolePicker.IsEnabled = true;
                myButton.IsEnabled = true;
                indicator.IsRunning = false;
                indicator.IsVisible = false;
                return false;
            }
        }
        async Task PutTaskDelay(int delay)
        {
            await Task.Delay(delay);
        }

    }
}