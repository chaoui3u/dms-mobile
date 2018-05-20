using MeteoMobile.Helpers;
using MeteoMobile.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MeteoMobile.ViewModels
{
    class SignUpViewModel
    {
        ApiServices _apiServices = new ApiServices();  

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public string Message { get; set; }
        


        public ICommand SignUpCommand
        {
            get
            {
                return new Command(async() => 
                {
                    
                        var isSuccess = await _apiServices.SignUpAsync(Settings.AccessToken,FirstName,
                            LastName, Password, Email, Role);
                    
                });
          
            }
        }

      
    }
}
