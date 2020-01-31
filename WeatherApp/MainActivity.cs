using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Locations;
using WeatherApp.Model;
using System;
using Newtonsoft.Json;
using Android.Content;

namespace WeatherApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, ILocationListener
    {
        TextView textCity, textLastUpdate, textDescription, textDegree, textTime, textHumidity;
        ImageView ImageView1;

        LocationManager LocMan;
        string provider;
        static double lat, lon;
        WMap wMap = new WMap();

        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            LocMan = (LocationManager)GetSystemService(Context.LocationService);
            provider = LocMan.GetBestProvider(new Criteria(), false);

            Location location = LocMan.GetLastKnownLocation(provider);
            if(location == null)
            {
                System.Diagnostics.Debug.WriteLine("No location")
            }

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnResume()
        {
            base.OnResume();
            LocMan.RequestLocationUpdates(provider, 400, 1, this);
        }
        protected override void OnPause()
        {
            base.OnPause();
            LocMan.RemoveUpdates(this);
        }

        public void OnLocationChanged(Location location)
        {
            lat = Math.Round(location.Latitude, 4);
            lon = Math.Round(location.Longitude, 4);

        }

        public void OnProviderDisabled(string provider)
        {
        }

        public void OnProviderEnabled(string provider)
        {
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
        }

        private class GetW : AsyncTask<String, Java.Lang.Void, String>
        {
            [Obsolete]
            private ProgressDialog pd = new ProgressDialog(Application.Context);
            private MainActivity activity;
            WMap wMap;
            public GetW(MainActivity activity, WMap wMap)
            {
                this.activity = activity;
                this.wMap = wMap;
            }

            [Obsolete]
            protected override void OnPreExecute()
            {
                base.OnPreExecute();
                pd.Window.SetType(Android.Views.WindowManagerTypes.SystemAlert);
                pd.SetTitle("Location Check...");
                pd.Show();
            }

            protected override string RunInBackground(params string[] @params)
            {
                string stream = null;
                string urlstring = @params[0];

                //urlstring = Common.Common.APIRequest(lat.ToString(), lon.ToString());
                Helper.Help http = new Helper.Help();
                stream = http.GetHTTP(urlstring);
                return stream;
            }

            [Obsolete]
            protected override void OnPostExecute(string result)
            {
                base.OnPostExecute(result);
                if(result.Contains("Error: not found city"))
                {
                    pd.Dismiss();
                    return;
                }
                wMap = JsonConvert.DeserializeObject<WMap> (result);
                pd.Dismiss();

                //Control
                activity.textCity = activity.FindViewById<TextView>(Resource.Id.textCity);
                activity.textLastUpdate = activity.FindViewById<TextView>(Resource.Id.textLastUpdate);
                activity.textDescription = activity.FindViewById<TextView>(Resource.Id.textDescription);
                activity.textHumidity = activity.FindViewById<TextView>(Resource.Id.textHumidity);
                activity.textTime = activity.FindViewById<TextView>(Resource.Id.textTime);
                activity.textDegree = activity.FindViewById<TextView>(Resource.Id.textDegree);


                activity.ImageView1 = activity.FindViewById<ImageView>(Resource.Id.imageView1);

                //AddText
                activity.textCity.Text = $"{wMap.name},{wMap.sys.country}";
                activity.textLastUpdate.Text = $"Last Updated: {DateTime.Now.ToString("dd MMMM yyyy HH:mm")}";
                activity.textDescription.Text = $"{wMap.weather[0].description}";
                activity.textHumidity.Text = $"Humidity: {wMap.main.humidity} %";
                activity.textTime.Text = $"{Common.Common.UnixTimeStampToDateTime(wMap.sys.sunrise)}/{Common.Common.UnixTimeStampToDateTime(wMap.sys.sunset)}";
                activity.textDegree.Text = $"{wMap.main.temp} °C";

                if (!String.IsNullOrEmpty(wMap.weather[0].icon))
                {
                    Android.Net.Uri url = Android.Net.Uri.Parse(Common.Common.GetImage(wMap.weather[0].icon));
                    activity.ImageView1.SetImageURI(url);
                }


                return;

            }
        }

    }
}