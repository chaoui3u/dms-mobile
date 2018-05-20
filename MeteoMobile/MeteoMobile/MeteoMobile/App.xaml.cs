using MeteoMobile.Helpers;
using MeteoMobile.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace MeteoMobile
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            MainPage = new LoginPage();
           // SetMainPage();
        }

        private void SetMainPage()
        {
            //if (DateTime.UtcNow >= Settings.AccessTokenExpirationDate)
            //    MainPage = new LoginPage();

        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
