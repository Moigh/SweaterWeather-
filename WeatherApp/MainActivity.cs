﻿using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Locations;
using WeatherApp.Model;
using System;

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
    }
}