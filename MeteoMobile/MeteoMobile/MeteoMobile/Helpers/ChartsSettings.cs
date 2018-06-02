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
            float Pressure=0;
            for (var i = 0; i < columnNumber; i++)
            {
                Pressure += _weatherRecords[i].MainData.Pressure;
                if ((i + 1) % 18 == 0)
                {
                    entries.Add(new Entry(Pressure / 18)
                    {
                        Color = SKColor.Parse("#00CED1"),
                        Label = _weatherRecords[i].CurrentTime.Hour.ToString("00.##") + "H",
                        ValueLabel = _weatherRecords[i].MainData.Pressure + " hpa"
                    });
                    Pressure = 0;
                }
            }
            return entries;
        }

        public List<Entry> WindSpeed(int columnNumber)
        {
            var entries = new List<Entry>();
            float WindSpeed = 0;
            for (var i = 0; i < columnNumber; i++)
            {
                WindSpeed += _weatherRecords[i].Wind.Speed;
                if ((i + 1) % 18 == 0)
                {
                    entries.Add(new Entry(WindSpeed/18)
                    {
                        Color = SKColor.Parse("#800000"),
                        Label = _weatherRecords[i].CurrentTime.Hour.ToString("00.##") + "H",
                        ValueLabel = _weatherRecords[i].Wind.Speed + " m/s"
                    });
                    WindSpeed = 0;
                }
            }
            return entries;
        }

        public List<Entry> Temprature(int columnNumber)
        {
            var entries = new List<Entry>();
            float Temprature = 0;
            for (var i = 0; i < columnNumber; i++)
            {
                Temprature += _weatherRecords[i].MainData.Temp;
                if ((i + 1) % 18 == 0)
                {
                    entries.Add(new Entry(Temprature/18)
                    {
                        Color = SKColor.Parse("#00BFFF"),
                        Label = _weatherRecords[i].CurrentTime.Hour.ToString("00.##") + "H",
                        ValueLabel = _weatherRecords[i].MainData.Temp + " °C"
                    });
                    Temprature = 0;
                }
            }
            return entries;
        }

        public List<Entry> WindDirection(int columnNumber)
        {
            var entries = new List<Entry>();
            float WindDegree = 0;
            for (var i = 0; i < columnNumber; i++)
            {
                WindDegree += _weatherRecords[i].Wind.Degree;
                if ((i + 1) % 18 == 0)
                {
                    entries.Add(new Entry(WindDegree/18)
                    {
                        Color = SKColor.Parse("#A52A2A"),
                        Label = _weatherRecords[i].CurrentTime.Hour.ToString("00.##") + "H",
                        ValueLabel = _weatherRecords[i].Wind.Degree + " °"
                    });
                    WindDegree = 0;
                }
            }
            return entries;
        }

        public List<Entry> Humidity(int columnNumber)
        {
            var entries = new List<Entry>();
            int Humidity = 0;
            for (var i = 0; i < columnNumber; i++)
            {
                Humidity += _weatherRecords[i].MainData.Humidity;
                if ((i+1) % 18 == 0)
                {
                    entries.Add(new Entry(Humidity/18)
                    {
                        Color = SKColor.Parse("#B22222"),
                        Label = _weatherRecords[i].CurrentTime.Hour.ToString("00.##") + "H",//+ _weatherRecords[i].CurrentTime.Minute.ToString(),
                        ValueLabel = _weatherRecords[i].MainData.Humidity + " %"
                    });
                    Humidity = 0;
                }
            }
            return entries;
        }
    }
}
