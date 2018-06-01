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
    class HomeViewModel : INotifyPropertyChanged
    {
        ApiServices _apiServices = new ApiServices();
        private float _temperature;
        private float _pressure;
        private int _humidity;
        private float _windSpeed;
        private float _windDegree;
        private bool _isSuccess;
        private List<WeatherRecordModel> _weatherRecord;

        public List<WeatherRecordModel> WeatherRecord
        {
            get { return _weatherRecord; }
            set { _weatherRecord = value;OnPropertyChanged(nameof(WeatherRecord)); }
        }

        private DateTimeOffset _currentTime;
        public DateTimeOffset CurrentTime
        {
            get { return _currentTime; }
            set { _currentTime = value; OnPropertyChanged(nameof(CurrentTime)); }
        }

        public float Temperature
        {
            get { return _temperature; }
            set { _temperature = value; OnPropertyChanged(nameof(Temperature)); }

        }
        public float Pressure
        {
            get { return _pressure; }
            set { _pressure = value; OnPropertyChanged(nameof(Pressure)); }

        }
        public int Humidity
        {
            get { return _humidity; }
            set { _humidity = value; OnPropertyChanged(nameof(Humidity)); }

        }
        public float WindSpeed
        {
            get { return _windSpeed; }
            set { _windSpeed = value; OnPropertyChanged(nameof(WindSpeed)); }

        }

        public float WindDegree
        {
            get { return _windDegree; }
            set { _windDegree = value;OnPropertyChanged(nameof(WindDegree)); }
        }

        public bool IsSuccess
        {
            get { return _isSuccess; }
            set { _isSuccess = value;OnPropertyChanged(nameof(IsSuccess)); }

        }

        public ICommand GetActualWeatherRecord
        {
            get
            {
                return new Command(async () => 
                {
                    WeatherRecord = await _apiServices.GetActualWeatherRecord(Settings.AccessToken, DateTimeOffset.Now);
                    if (WeatherRecord != null)
                    {
                        Temperature = WeatherRecord[0].MainData.Temp;
                        Pressure = WeatherRecord[0].MainData.Pressure;
                        Humidity = WeatherRecord[0].MainData.Humidity;
                        WindSpeed = WeatherRecord[0].Wind.Speed;
                        WindDegree = WeatherRecord[0].Wind.Degree;
                        CurrentTime = WeatherRecord[0].CurrentTime;
                        IsSuccess = true;
                    }
                    else IsSuccess = false;
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
