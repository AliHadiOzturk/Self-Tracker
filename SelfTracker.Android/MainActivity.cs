using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Gms.Common;
using Android.Util;
using Firebase.Iid;
using Firebase.Messaging;
using Android.Media;
using Android.Content.PM;

namespace SelfTracker.Droid
{
    [Activity(Label = "SelfTracker", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        const string TAG = "MainActivity";
        internal static readonly string CHANNEL_ID = "my_notification_channel";
        internal static readonly int NOTIFICATION_ID = 100;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            #region FCM
            CreateNotificationChannel();
            IsPlayServicesAvailable();


            Log.Debug(TAG, "InstanceID token: " + FirebaseInstanceId.Instance.Id);

            FirebaseMessaging.Instance.SubscribeToTopic("news");
            Log.Debug(TAG, "Subscribed to remote notifications");

            #endregion




            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public bool IsPlayServicesAvailable()
        {
            var resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                {
                    //msgText.Text = GoogleApiAvailability.Instance.GetErrorString(resultCode);
                }
                else
                {
                    //msgText.Text = "This device is not supported";
                    Finish();
                }

                return false;
            }

            //msgText.Text = "Google Play Services is available.";
            return true;
        }


        void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification 
                // channel on older versions of Android.
                return;
            }

            var alarmAttributes = new AudioAttributes.Builder()
                    .SetContentType(AudioContentType.Sonification)
                    .SetUsage(AudioUsageKind.Notification).Build();

            // Create the uri for the alarm file                 
            Android.Net.Uri alarmUri = Android.Net.Uri.Parse($"{ContentResolver.SchemeAndroidResource}://{Application.Context.PackageName}/{Resource.Raw.soundFile}");

            var channel = new NotificationChannel(CHANNEL_ID, "SelfTrackerNotificationChannel", NotificationImportance.Default)
            {
                Description = "Notification Channel"
            };
            channel.SetSound(alarmUri, alarmAttributes);

            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }
    }
}