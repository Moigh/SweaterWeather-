using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace WeatherApp.Common
{
    class Common
    {
        public static string API_KEY = "161d17c2537e0ae2e8cbf9c3e9252b9d";
        public static string API_LINK = "http://api.openweathermap.org/data/2.5/weather";

        public static string APIRequest(string lat, string lng)
        {
            StringBuilder sb = new StringBuilder(API_LINK);
            sb.AppendFormat("?lat={0}&lon={1}&APPID{2}&units=metric");
            return sb.ToString();

        }

        public static string UTimeToDate ()

    }
}