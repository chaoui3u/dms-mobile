using MeteoMobile.Helpers;
using MeteoMobile.Models;
using MeteoMobile.ViewModels;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace MeteoMobile.Views
{
   
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatisticsTabbedPageDetail : TabbedPage
    {
        WeatherRecordViewModel vm = new WeatherRecordViewModel();
        //DateTimeOffset dateCaptured;
        public StatisticsTabbedPageDetail ()
        {
            InitializeComponent();
           
     
        }
        protected override  void OnAppearing()
        {
           
            picker.MaximumDate = DateTimeOffset.UtcNow.Date;
            Device.BeginInvokeOnMainThread(() => 
            {
                picker.Focus();
            });
 
        }

        async Task PutTaskDelay(int delay)
        {
            await Task.Delay(delay);
        }

        private async void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            //dateCaptured = e.NewDate;
            vm.DateTimeChosen = e.NewDate;
            vm.GetWeatherRecordsCommand.Execute(null);
            await PutTaskDelay(2400);
            if (vm.WeatherRecords == null)
                await PutTaskDelay(1000);
            if (vm.WeatherRecords == null || vm.WeatherRecords.Count() == 0)
            {
                await DisplayAlert("Erreur 404", "Rien n'a était touvé à cette date.", "ok");
            }
            else
            {
                var weatherRecords = vm.WeatherRecords;

                var charts = new ChartsSettings(weatherRecords);


                tempratureChart.Chart = new PointChart { Entries = charts.Temprature(weatherRecords.Count()) };
                windDirectionChart.Chart = new LineChart { Entries = charts.WindDirection(weatherRecords.Count()) };
                windChart.Chart = new LineChart { Entries = charts.WindSpeed(weatherRecords.Count()) };
                humidityChart.Chart = new BarChart { Entries = charts.Humidity(weatherRecords.Count()) };
                pressureChart.Chart = new PointChart { Entries = charts.Pressure(weatherRecords.Count()) };
            }
        }

        private void ToolPicker_Activated(object sender, EventArgs e)
        {
            picker.MaximumDate = DateTimeOffset.UtcNow.Date;
            Device.BeginInvokeOnMainThread(() =>
            {
                picker.Focus();
            });
        }
    }
}