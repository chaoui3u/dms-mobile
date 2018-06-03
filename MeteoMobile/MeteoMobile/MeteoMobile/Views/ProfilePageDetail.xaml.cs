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
	public partial class ProfilePageDetail : ContentPage
	{
        ProfileViewModel vm;
        public ProfilePageDetail ()
		{
			InitializeComponent ();

            
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();
            vm = (ProfileViewModel)this.BindingContext;
            vm.GetCurrentUserCommand.Execute(null);
            //await PutTaskDelay(400);
            nameLabel.Text = vm.FirstName + " " + vm.LastName;
        }

        async Task PutTaskDelay(int delay)
        {
            await Task.Delay(delay);
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ModifyUserPage()));
        }
    }
}