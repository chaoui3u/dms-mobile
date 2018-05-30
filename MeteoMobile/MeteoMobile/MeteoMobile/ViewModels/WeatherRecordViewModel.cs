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
    public class WeatherRecordViewModel : INotifyPropertyChanged
    {
   
        ApiServices _apiServices = new ApiServices();
        private List<WeatherRecordModel> _weatherRecords;
        private DateTimeOffset _dateTime;
        
        public DateTimeOffset DateTimeChosen
        {
            get { return _dateTime;  }
            set { _dateTime = value; OnPropertyChanged(nameof(DateTimeChosen)); }
        }

        public List<WeatherRecordModel> WeatherRecords
        {
            get { return _weatherRecords; }
            set { _weatherRecords = value; OnPropertyChanged(nameof(WeatherRecords));}
        }
        private bool _isSuccess;
        public bool IsSuccess
        {
            get { return _isSuccess; }
            set { _isSuccess = value; OnPropertyChanged(nameof(IsSuccess)); }
        }
        public ICommand GetWeatherRecordsCommand
        {
            get
            {
                return new Command(async () =>
                {
                    WeatherRecords = await _apiServices.GetWeatherRecordsAsync(Settings.AccessToken,DateTimeChosen);
                    if (WeatherRecords == null || WeatherRecords.Count == 0) IsSuccess = false;
                    else IsSuccess = true;
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
