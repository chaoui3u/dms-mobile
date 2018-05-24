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
using Entry = Microcharts.Entry;

namespace MeteoMobile.Views
{
   
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatisticsTabbedPageDetail : TabbedPage
    {
        WeatherRecordViewModel vm = new WeatherRecordViewModel();

        public StatisticsTabbedPageDetail ()
        {
            InitializeComponent();
           
     
        }
        protected override async void OnAppearing()
        {
            vm.GetWeatherRecordsCommand.Execute(null);
            await PutTaskDelay();
            if (vm.WeatherRecords == null)
            await PutTaskDelay();
            var weatherRecords = vm.WeatherRecords;
            var entriesDemo = new List<Entry>();

            for (var i = 0; i <= 10; i++)
            {
                entriesDemo.Add(new Entry(weatherRecords[i].Clouds.All)
                {
                    Color = SKColor.Parse("#00BFFF"),
                    Label = i.ToString(), //weatherRecords[i].CurrentTime.Hour.ToString() + "H"+ weatherRecords[i].CurrentTime.Minute.ToString(),
                    ValueLabel = weatherRecords[i].Clouds.All + " %"
                });
            }
            var entriesDemo2 = new List<Entry>();
            for (var i = 0; i <= 5; i++)
            {
                entriesDemo2.Add(new Entry(weatherRecords[i].MainData.Humidity)
                {
                    Color = SKColor.Parse("#00CED1"),
                    Label = i.ToString(), //weatherRecords[i].CurrentTime.Hour.ToString() + "H"+ weatherRecords[i].CurrentTime.Minute.ToString(),
                    ValueLabel = weatherRecords[i].MainData.Humidity + " %"
                });
            }
            var entriesDemo3 = new List<Entry>();
            for (var i = 0; i <= 10; i++)
            {
                entriesDemo3.Add(new Entry(weatherRecords[i].Wind.Speed)
                {
                    Color = SKColor.Parse("#FF1493"),
                    Label = i.ToString(), //weatherRecords[i].CurrentTime.Hour.ToString() + "H"+ weatherRecords[i].CurrentTime.Minute.ToString(),
                    ValueLabel = weatherRecords[i].Wind.Speed + " m/s"
                });
            }


            Chart1.Chart = new PointChart { Entries = entriesDemo };
            Chart2.Chart = new BarChart { Entries = entriesDemo2 };
            Chart3.Chart = new LineChart { Entries = entriesDemo3 };
        }

            async Task PutTaskDelay()
        {
            await Task.Delay(2400);
        }
    }
}