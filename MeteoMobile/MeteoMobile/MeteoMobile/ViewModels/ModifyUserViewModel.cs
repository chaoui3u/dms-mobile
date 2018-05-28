using MeteoMobile.Helpers;
using MeteoMobile.Models;
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
    public class ModifyUserViewModel 
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



        public ICommand ModifyUserCommand
        {
            get
            {
                return new Command(async () =>
                {

                var isSuccess = await _apiServices.ModifyUserAsync(Settings.AccessToken, 
                        Constants.ThiUser.Id,FirstName,
                        LastName, Password, Email, Role);
                    

                });

            }

        }
    }
}
