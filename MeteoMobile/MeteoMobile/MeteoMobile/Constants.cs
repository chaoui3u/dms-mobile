using MeteoMobile.Models;
using MeteoMobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeteoMobile
{
    public class Constants
    {
        public static string BaseApiAddress => "https://192.168.0.103:44320/";
        public static string SignUpUrl => "https://192.168.0.103:44320/authentification/signup/";
        public static string getUsersUrl => "https://192.168.0.103:44320/authentification/users/";
        public static string getWeatherRecordsUrl => "https://192.168.0.103:44320/weatherrecord/";
        public static string getMyUserUrl => "https://192.168.0.103:44320/authentification/users/me/";
        public static UserModel userStatic { get; set; }
    }
}
