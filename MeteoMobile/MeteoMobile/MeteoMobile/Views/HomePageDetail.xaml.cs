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
    public partial class HomePageDetail : ContentPage
    {
        HomeViewModel vm;
        public HomePageDetail()
        {
            InitializeComponent();
            nameLabel.Text = Constants.MyUser.FirstName + " " + Constants.MyUser.LastName;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            vm = (HomeViewModel)this.BindingContext;
            vm.GetActualWeatherRecord.Execute(null);
            await PutTaskDelay(4000);
            if(vm.IsSuccess == false) {
                await DisplayAlert("Echec", "Aucune données n'a était" +
                " trouvé dans les dérinière ving minute", "Ok");
            }
        }
        async Task PutTaskDelay(int delay)
        {
            await Task.Delay(delay);
        }
    }
}