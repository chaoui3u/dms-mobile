using MeteoMobile.Helpers;
using MeteoMobile.Models;
using MeteoMobile.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
            set { _users = value; OnPropertyChanged(nameof(Users)); }
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; OnPropertyChanged(nameof(IsBusy)); }
        }
        private bool _isSuccess;
        public bool IsSuccess
        {
            get { return _isSuccess; }
            set { _isSuccess = value; OnPropertyChanged(nameof(IsSuccess)); }
        }

        //public UsersViewModel()
        //{
        //    GetUsersCommand.Execute(null);
        //}

        public ICommand GetUsersCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsBusy = true;
                    Users = await _apiServices.GetUsersAsync(Settings.AccessToken);
                    await PutTaskDelay(500);
                    IsBusy = false;
                });
            }
        }
        public ICommand DeleteUserCommand
        {
            get
            {
                return new Command(async () => 
                {
                    IsBusy = true;
                    //await PutTaskDelay(2000);
                    IsSuccess= await _apiServices.DeleteUserAsync(Settings.AccessToken, Constants.ThisUser.Id);
                    if (IsSuccess)
                    {
                        GetUsersCommand.Execute(null);
                    }
                });
            }
        }
        async Task PutTaskDelay(int delay)
        {
            await Task.Delay(delay);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
