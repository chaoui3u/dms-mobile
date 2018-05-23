using System;
using System.Collections.Generic;
using System.Text;

namespace MeteoMobile.Models
{
    public class WeatherRecordModel
    {

        public CloudsModel Clouds { get; set; }
        public MainDataModel MainData { get; set; }
        public RainModel Rain { get; set; }
        public SnowModel Snow { get; set; }
        public SunModel Sun { get; set; }
        public WindModel Wind { get; set; }
        public DateTimeOffset CurrentTime { get; set; }

    }
}
