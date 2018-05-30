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
        public bool IsSuccess { get; set; }


        public ICommand ModifyUserCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if(Constants.ThisUser != null)
                     IsSuccess = await _apiServices.ModifyUserAsync(Settings.AccessToken, 
                        Constants.ThisUser.Id,FirstName,
                        LastName, Password, Email, Role);
                    else
                        IsSuccess = await _apiServices.ModifyUserAsync(Settings.AccessToken,
                        Constants.MyUser.Id, FirstName,
                        LastName, Password, Email, Role);
                        if(IsSuccess) Constants.MyUser = await _apiServices.GetMyUserAsync(Settings.AccessToken);
                    if (Constants.MyUser == null) Application.Current.MainPage = new LogoutPage();
                });

            }

        }
        async Task PutTaskDelay(int delay)
        {
            await Task.Delay(delay);
        }
    }
}
