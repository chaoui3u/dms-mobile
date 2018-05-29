using MeteoMobile.Helpers;
using MeteoMobile.Models;
using MeteoMobile.Services;
using MeteoMobile.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeteoMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsersPageDetail : ContentPage
    {
 
        public UsersPageDetail()
        {
            InitializeComponent();
     
        }

        protected override  void OnAppearing()
        {
            //getting the current view model bound to this page
            var vm = (UsersViewModel)this.BindingContext;
            vm.GetUsersCommand.Execute(null);
            //MyListView.ItemsSource = vm.Users;
        }
        async Task PutTaskDelay(int delay)
        {
            await Task.Delay(delay);
        }

        //async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        //{
        //    if (e.Item == null)
        //        return;

        //    await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

        //    //Deselect Item
        //    ((ListView)sender).SelectedItem = null;

 
        //}

        private async void Modify_Clicked(object sender, EventArgs e)
        {
            Constants.ThisUser = ((MenuItem)sender).CommandParameter as UserModel;

            await Navigation.PushModalAsync(new NavigationPage(new ModifyUserPage()));
        }
       
        private async void Delete_Clicked(object sender, EventArgs e)
        {
            Constants.ThisUser = ((MenuItem)sender).CommandParameter as UserModel;
            await PutTaskDelay(5000);
            var vm = (UsersViewModel)this.BindingContext;
            DisplayConfirmAlert(vm.IsSuccess);
        }

        private async void SignUpToolBar_Activated(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new SignUpPage()));
        }

        public void DisplayConfirmAlert(bool result)
        {
            if (result)
            {
                DisplayAlert("Succès", "Votre utilisateur à était supprimé avec succès", "Ok");
                Constants.CurrentResult = false;
            }
            else DisplayAlert("Echec", "Votre utilisateur n'a pas était supprimé", "Ok");
        }


    }
}
