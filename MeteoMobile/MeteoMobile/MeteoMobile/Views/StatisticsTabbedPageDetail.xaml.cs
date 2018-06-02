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
        WeatherRecordViewModel vm;
        //DateTimeOffset dateCaptured;
        public StatisticsTabbedPageDetail ()
        {
            InitializeComponent();
            vm = (WeatherRecordViewModel)this.BindingContext;
     
        }
        protected override  void OnAppearing()
        {
           
            picker.MaximumDate = DateTimeOffset.Now.Date;
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
            vm.DateTimeChosen = e.NewDate;
            vm.GetWeatherRecordsCommand.Execute(null);
             await PutTaskDelay(2000);
            var weatherRecords = vm.WeatherRecords;
            if (!vm.IsSuccess || weatherRecords.Count() < 18)
            {
                await DisplayAlert("Erreur 404", "Pas assez de valeur pour faire la moyenne de 3H.", "ok");
            
            }
            else
            {


                var charts = new ChartsSettings(weatherRecords);


                tempratureChart.Chart = new PointChart { Entries = charts.Temprature(weatherRecords.Count()) };
                windDirectionChart.Chart = new LineChart { Entries = charts.WindDirection(weatherRecords.Count()) };
                windChart.Chart = new LineChart { Entries = charts.WindSpeed(weatherRecords.Count()) };
                humidityChart.Chart = new BarChart { Entries = charts.Humidity(weatherRecords.Count()) };
                pressureChart.Chart = new PointChart { Entries = charts.Pressure(weatherRecords.Count()) };
                tempraturePage.IsBusy = false;
            }
        }

        private void ToolPicker_Activated(object sender, EventArgs e)
        {
            picker.MaximumDate = DateTimeOffset.Now.Date;
            Device.BeginInvokeOnMainThread(() =>
            {
                picker.Focus();
            });
        }
    }
}