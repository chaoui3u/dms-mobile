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
       // WeatherRecordViewModel vm = new WeatherRecordViewModel();

        List<Entry> entriesPage1 = new List<Entry>
    {
        new Entry(200)
        {
            Color = SKColor.Parse("#FF1493"),
            Label = "Data Sample 1",
            ValueLabel="200 test"
        },
        new Entry(150)
        {
            Color = SKColor.Parse("#00BFFF"),
            Label = "Data Sample 2",
            ValueLabel="150 test"
        },
        new Entry(300)
        {
            Color = SKColor.Parse("#00CED1"),
            Label = "Data Sample 3",
            ValueLabel="300 test"
        }
    };

        List<Entry> entriesPage2 = new List<Entry>
    {
        new Entry(200)
        {
            Color = SKColor.Parse("#FF1493"),
            Label = "Data Sample 1",
            ValueLabel="200 test"
        },
        new Entry(150)
        {
            Color = SKColor.Parse("#00BFFF"),
            Label = "Data Sample 2",
            ValueLabel="150 test"
        },
        new Entry(300)
        {
            Color = SKColor.Parse("#00CED1"),
            Label = "Data Sample 3",
            ValueLabel="300 test"
        }
    };

        List<Entry> entriesPage3 = new List<Entry>
    {
        new Entry(200)
        {
            Color = SKColor.Parse("#FF1493"),
            Label = "Data Sample 1",
            ValueLabel="200 test"
        },
        new Entry(150)
        {
            Color = SKColor.Parse("#00BFFF"),
            Label = "Data Sample 2",
            ValueLabel="150 test"
        },
        new Entry(300)
        {
            Color = SKColor.Parse("#00CED1"),
            Label = "Data Sample 3",
            ValueLabel="300 test"
        }
    };


        public StatisticsTabbedPageDetail ()
        {
            InitializeComponent();

            Chart1.Chart = new PointChart { Entries = entriesPage1, };
            Chart2.Chart = new BarChart { Entries = entriesPage2 };
            Chart3.Chart = new LineChart { Entries = entriesPage3 };

            //vm.GetWeatherRecordsCommand.Execute(null);
        }
    }
}