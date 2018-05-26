using MeteoMobile.Models;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Entry = Microcharts.Entry;

namespace MeteoMobile.Helpers
{
    class ChartsSettings
    {
        List<WeatherRecordModel> _weatherRecords;

        public ChartsSettings(List<WeatherRecordModel> weatherRecords)
        {
            _weatherRecords = weatherRecords;
        }

   

        public List<Entry> Pressure(int columnNumber)
        {
            var entries = new List<Entry>();

            for (var i = 0; i < columnNumber; i++)
            {
                entries.Add(new Entry(_weatherRecords[i].MainData.Pressure)
                {
                    Color = SKColor.Parse("#00CED1"),
                    Label = _weatherRecords[i].CurrentTime.Hour.ToString() + "H",
                    ValueLabel = _weatherRecords[i].MainData.Pressure + " hpa"
                });
            }
            return entries;
        }

        public List<Entry> WindSpeed(int columnNumber)
        {
            var entries = new List<Entry>();

            for (var i = 0; i < columnNumber; i++)
            {
                entries.Add(new Entry(_weatherRecords[i].Wind.Speed)
                {
                    Color = SKColor.Parse("#800000"),
                    Label = _weatherRecords[i].CurrentTime.Hour.ToString() + "H",
                    ValueLabel = _weatherRecords[i].Wind.Speed + " m/s"
                });
            }
            return entries;
        }

        public List<Entry> Temprature(int columnNumber)
        {
            var entries = new List<Entry>();

            for (var i = 0; i < columnNumber; i++)
            {
                entries.Add(new Entry(_weatherRecords[i].MainData.Temp)
                {
                    Color = SKColor.Parse("#00BFFF"),
                    Label = _weatherRecords[i].CurrentTime.Hour.ToString() + "H",
                    ValueLabel = _weatherRecords[i].MainData.Temp + " °C"
                });
            }
            return entries;
        }

        public List<Entry> WindDirection(int columnNumber)
        {
            var entries = new List<Entry>();

            for (var i = 0; i < columnNumber; i++)
            {
                entries.Add(new Entry(_weatherRecords[i].Wind.Degree)
                {
                    Color = SKColor.Parse("#A52A2A"),
                    Label = _weatherRecords[i].CurrentTime.Hour.ToString() + "H",
                    ValueLabel = _weatherRecords[i].Wind.Degree + " °"
                });
            }
            return entries;
        }

        public List<Entry> Humidity(int columnNumber)
        {
            var entries = new List<Entry>();

            for (var i = 0; i < columnNumber; i++)
            {
                entries.Add(new Entry(_weatherRecords[i].MainData.Humidity)
                {
                    Color = SKColor.Parse("#B22222"),
                    Label = _weatherRecords[i].CurrentTime.Hour.ToString() + "H",//+ _weatherRecords[i].CurrentTime.Minute.ToString(),
                    ValueLabel = _weatherRecords[i].MainData.Humidity + " %"
                });
            }
            return entries;
        }
    }
}
