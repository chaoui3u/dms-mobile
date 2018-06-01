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
    class ProfileViewModel : INotifyPropertyChanged
    {
        ApiServices _apiServices = new ApiServices();

        public ProfileViewModel()
        {

        }
        private UserModel _user;
        public UserModel User
        {
            get { return _user; }
            set { _user = value; OnPropertyChanged(nameof(User)); }
        }
        private bool _isSuccess;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsSuccess
        {
            get { return _isSuccess; }
            set { _isSuccess = value; OnPropertyChanged(nameof(IsSuccess)); }
        }
        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value;OnPropertyChanged(nameof(FirstName)); }
        }
        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; OnPropertyChanged(nameof(LastName)); }
        }
        private string _email;
        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(nameof(Email)); }
        }
        private string _role;
        public string Role
        {
            get { return _role; }
            set { _role = value; OnPropertyChanged(nameof(Role)); }
        }
        private DateTimeOffset _createdAt;
        public DateTimeOffset CreatedAt
        {
            get { return _createdAt; }
            set { _createdAt = value; OnPropertyChanged(nameof(CreatedAt)); }
        }


        public ICommand GetCurrentUserCommand
        {
            get
            {
                return new Command(async () => 
                {
                    User = await _apiServices.GetMyUserAsync(Settings.AccessToken);
                    if (User != null) 
                    {
                        FirstName = User.FirstName;
                        LastName = User.LastName;
                        Email = User.Email;
                        Role = User.Role;
                        CreatedAt = User.CreatedAt;
                        IsSuccess = true;
                    }
                    else IsSuccess = false;
                });
            }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
