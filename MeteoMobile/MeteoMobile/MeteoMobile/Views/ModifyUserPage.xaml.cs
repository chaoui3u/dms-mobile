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
            firstName.Text = Constants.ThisUser.FirstName;
            lastName.Text = Constants.ThisUser.LastName;
            password.Text = Settings.Password;
            confirmPassword.Text = Settings.Password;
            email.Text = Constants.ThisUser.Email;
            rolePicker.SelectedItem = Constants.ThisUser.Role;
        }
    }
}