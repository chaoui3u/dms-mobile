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
            set { _weatherRecord = value; OnPropertyChanged(nameof(WeatherRecord)); }
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
        private string _tempCelcius;
        public string TempCelcius
        {
            get { return _tempCelcius; }
            set { _tempCelcius = value; OnPropertyChanged(nameof(TempCelcius)); }
        }
        
       
        public float Pressure
        {
            get { return _pressure; }
            set { _pressure = value; OnPropertyChanged(nameof(Pressure)); }

        }
        private string _pressurePascal;
        public string PressurePascal
        {
            get { return _pressurePascal; }
            set { _pressurePascal = value; OnPropertyChanged(nameof(PressurePascal)); }
        }
        public int Humidity
        {
            get { return _humidity; }
            set { _humidity = value; OnPropertyChanged(nameof(Humidity)); }

        }
        private string _humidityPercent;
        public string HumidityPercent
        {
            get { return _humidityPercent; }
            set { _humidityPercent = value; OnPropertyChanged(nameof(HumidityPercent)); }
        }
        public float WindSpeed
        {
            get { return _windSpeed; }
            set { _windSpeed = value; OnPropertyChanged(nameof(WindSpeed)); }

        }
        private string _windSpeedMeterPerSec;
        public string WindSpeedMeterPerSec
        {
            get { return _windSpeedMeterPerSec; }
            set { _windSpeedMeterPerSec = value; OnPropertyChanged(nameof(WindSpeedMeterPerSec)); }
        }

        public float WindDegree
        {
            get { return _windDegree; }
            set { _windDegree = value;OnPropertyChanged(nameof(WindDegree)); }
        }
        private string _windDirectionDegree;
        public string WindDirectionDegree
        {
            get { return _windDirectionDegree; }
            set { _windDirectionDegree = value; OnPropertyChanged(nameof(WindDirectionDegree)); }
        }
        private string _timeOfDay;
        public string TimeOfDay
        {
            get { return _timeOfDay; }
            set { _timeOfDay = value; OnPropertyChanged(nameof(TimeOfDay)); }

        }
        private string _dateOfDay;
        public string DateOfDay
        {
            get { return _dateOfDay; }
            set { _dateOfDay = value; OnPropertyChanged(nameof(DateOfDay)); }
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
                    if (WeatherRecord != null && WeatherRecord.Count >= 1)
                    {
                        try
                        {
                            Temperature = WeatherRecord[0].MainData.Temp;
                            Pressure = WeatherRecord[0].MainData.Pressure;
                            Humidity = WeatherRecord[0].MainData.Humidity;
                            WindSpeed = WeatherRecord[0].Wind.Speed;
                            WindDegree = WeatherRecord[0].Wind.Degree;
                            CurrentTime = WeatherRecord[0].CurrentTime;
                            TempCelcius = string.Format("{0} °C", Temperature);
                            PressurePascal = string.Format("{0} hpa", Pressure);
                            HumidityPercent = string.Format("{0} %", Humidity);
                            WindSpeedMeterPerSec = string.Format("{0} m/s", WindSpeed);
                            WindDirectionDegree = string.Format("{0} °", WindDegree);
                            DateOfDay = string.Format("{0}/{1}/{2}",
                                CurrentTime.Date.Day.ToString("00.##"),
                                CurrentTime.Date.Month.ToString("00.##"),
                                CurrentTime.Date.Year);
                            TimeOfDay = string.Format("{0}:{1}:{2}",
                                CurrentTime.TimeOfDay.Hours.ToString("00.##"),
                                CurrentTime.TimeOfDay.Minutes.ToString("00.##"),
                                CurrentTime.TimeOfDay.Seconds.ToString("00.##"));
                            IsSuccess = true;
                        }
                        catch (Exception e)
                        {
                            System.Diagnostics.Debug.WriteLine("Exception Message : " + e.Message
                            + " Instance that caused the current message : "
                            + e.InnerException + " Stack of Exception :"
                            + e.StackTrace);
                            IsSuccess = false;
                        }
                        
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
