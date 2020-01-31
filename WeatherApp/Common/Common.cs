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

        public static string APIRequest(string lat, string lon)
        {
            StringBuilder s = new StringBuilder(API_LINK);
            s.AppendFormat($"?lat={0}&lon={1}&APPID{2}&units=metric", lat, lon, API_KEY);
            return s.ToString();

        }

        public static DateTime UTimeToDate (double uTime)
        {
            DateTime date = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            date.AddSeconds(uTime).ToLocalTime();
            return date;
        }

        public static string GetImage(string icon)
        {
            return $"http://openweathermap.org/img/w/{icon}.png";
        }

        internal static object UnixTimeStampToDateTime(int sunrise)
        {
            throw new NotImplementedException();
        }
    }
}