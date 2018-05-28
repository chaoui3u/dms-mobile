using MeteoMobile.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using MeteoMobile.Helpers;

namespace MeteoMobile.ViewModels
{
    class LoginViewModel
    {
        private readonly ApiServices _apiServices = new ApiServices();

        public string Username { get; set; }
        public string Password { get; set; }
        public ICommand LoginCommand
        {
            get
            {
                return new Command(async () =>
                {
       
                    Settings.AccessToken = await _apiServices.LoginAsync(Username, Password);
                    
                });
            }
        }

        //public LoginViewModel()
        //{
        //    Username = Settings.Username;
        //    Password = Settings.Password;
        //}
    }
}
