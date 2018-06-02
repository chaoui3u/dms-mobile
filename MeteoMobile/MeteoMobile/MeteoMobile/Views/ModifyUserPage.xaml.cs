﻿using MeteoMobile.Helpers;
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
                if (Constants.MyUser.Role == "Staff")
                    rolePicker.IsVisible = false;
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await PutTaskDelay(5000);
            var vm = (ModifyUserViewModel)this.BindingContext;
            DisplayConfirmAlert(vm.IsSuccess);
            Constants.ThisUser = null;
            await App.Current.MainPage.Navigation.PopModalAsync();
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