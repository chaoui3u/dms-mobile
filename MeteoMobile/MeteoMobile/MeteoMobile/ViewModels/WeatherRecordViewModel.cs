﻿using MeteoMobile.Helpers;
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
            set { _dateTime = value; OnPropertyChanged(); }
        }

        public List<WeatherRecordModel> WeatherRecords
        {
            get { return _weatherRecords; }
            set { _weatherRecords = value; OnPropertyChanged();}
        }

        public ICommand GetWeatherRecordsCommand
        {
            get
            {
                return new Command(async () =>
                {
                    WeatherRecords = await _apiServices.GetWeatherRecordsAsync(Settings.AccessToken,DateTimeChosen);
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
