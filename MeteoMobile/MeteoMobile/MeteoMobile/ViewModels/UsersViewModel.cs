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
    public class UsersViewModel : INotifyPropertyChanged
    {
        ApiServices _apiServices = new ApiServices();
        private List<UserModel> _users;

        public List<UserModel> Users {
            get { return _users; }
            set { _users = value; OnPropertyChanged(); }
        }

        public ICommand GetUsersCommand
        {
            get
            {
                return new Command(async () =>
                {
                    Users = await _apiServices.GetUsersAsync(Settings.AccessToken);
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
