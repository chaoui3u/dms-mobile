using MeteoMobile.Helpers;
using MeteoMobile.Models;
using MeteoMobile.Services;
using MeteoMobile.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MeteoMobile.ViewModels
{
    class SignUpViewModel 
    {
        ApiServices _apiServices = new ApiServices();

        public string FirstName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }
        public string Password
        {
            get;
            set;
        }

        public string ConfirmPassword
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public string Role
        {
            get;
            set;
        }
        


        public ICommand SignUpCommand
        {
            get
            {
                return new Command(async() => 
                {
                    
                   var isSuccess = await _apiServices.SignUpAsync(Settings.AccessToken,FirstName,
                            LastName, Password, Email, Role);
                    if (isSuccess) Constants.CurrentResult = true;
                    else Constants.CurrentResult = false;
                });
          
            }
        }
        async Task PutTaskDelay(int delay)
        {
            await Task.Delay(delay);
        }

    }
}
