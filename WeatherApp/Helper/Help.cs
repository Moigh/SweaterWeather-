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
using Java.IO;
using Java.Net;

namespace WeatherApp.Helper
{
    class Help
    {
        static String stream = null;

        public Help() { }

        public string GetHTTP(string webpage)
        {
            try{
                URL url = new URL(webpage);
                using (var URLconnect = (HttpURLConnection)url.OpenConnection())
                {
                    if (URLconnect.ResponseCode == HttpStatus.Ok) ;
                    BufferedReader br = new BufferedReader(new InputStreamReader(URLconnect.InputStream));
                    StringBuilder s = new StringBuilder();
                    string line;
                    while ((line = br.ReadLine()) != null)
                        s.Append(line);
                    stream = s.ToString();
                    URLconnect.Disconnect();
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return stream;
        }
    }
}