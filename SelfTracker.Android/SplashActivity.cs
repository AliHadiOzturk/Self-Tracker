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

namespace SelfTracker.Droid
{
    [Activity(Label = "Self Tracker", Icon = "@mipmap/icon", Theme = "@style/SelfTrackerTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            StartActivity(typeof(MainActivity));
            Finish();
            //StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}