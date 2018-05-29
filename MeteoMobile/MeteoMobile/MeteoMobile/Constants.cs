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
        public static string GetUsersUrl => "https://192.168.0.103:44320/authentification/users/";
        public static string GetWeatherRecordsUrl => "https://192.168.0.103:44320/weatherrecord/";
        public static string GetMyUserUrl => "https://192.168.0.103:44320/authentification/users/me/";
        public static UserModel ThisUser { get; set; }
        public static UserModel MyUser { get; set; }
        public static bool CurrentResult { get; set; }
    }
}
