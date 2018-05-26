using System;
using System.Collections.Generic;
using System.Text;

namespace MeteoMobile.Models
{
    public class WeatherRecordModel
    {

        public MainDataModel MainData { get; set; }
        public WindModel Wind { get; set; }
        public DateTimeOffset CurrentTime { get; set; }

    }
}
