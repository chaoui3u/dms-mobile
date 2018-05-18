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
    class SignUpViewModel //: INotifyPropertyChanged
    {
        ApiServices _apiServices = new ApiServices();  

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public string Message
        {
            get; set;
        }


        public ICommand SignUpCommand
        {
            get
            {
                return new Command(async() => 
                {
                    if (object.Equals(Password, ConfirmPassword))
                    {
                        var isSuccess = await _apiServices.SignUpAsync(Settings.AccessToken,FirstName,
                            LastName, Password, Email, Role);

                        if (isSuccess)
                        {
                            Message = "Utilisateur bien enregistré";
                        }
                        else
                        {
                            Message = "Utilisateur non enregistré";
                        }
                    }
                    else
                    {
                        Message = "La confirmation du mot de passe est fausse";
                    }
                });

                
            }
        }

        //public event PropertyChangedEventHandler PropertyChanged;

        //protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}
