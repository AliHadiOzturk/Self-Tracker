﻿using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Support.V4.App;
using Android.Util;

using Firebase.Messaging;

namespace SelfTracker.Droid.Firebase
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        const string TAG = "MyFirebaseMsgService";

        public override void OnMessageReceived(RemoteMessage message)
        {
            Log.Debug(TAG, "From: " + message.From);

            var body = message.GetNotification().Body;
            Log.Debug(TAG, "Notification Message Body: " + body);
            SendNotification(body, message.Data);
        }

        void SendNotification(string messageBody, IDictionary<string, string> data)
        {
            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            foreach (var key in data.Keys)
            {
                intent.PutExtra(key, data[key]);
            }
            // Creating an Audio Attribute
            var alarmAttributes = new AudioAttributes.Builder()
                .SetContentType(AudioContentType.Sonification)
                .SetUsage(AudioUsageKind.Notification).Build();

            // Create the uri for the alarm file                 
            Android.Net.Uri alarmUri = Android.Net.Uri.Parse($"{ ContentResolver.SchemeAndroidResource}://{Application.Context.PackageName}/{Resource.Raw.soundFile}");

            var pendingIntent = PendingIntent.GetActivity(this, MainActivity.NOTIFICATION_ID, intent, PendingIntentFlags.OneShot);

            var notificationBuilder = new NotificationCompat.Builder(this, MainActivity.CHANNEL_ID)
                                      .SetSmallIcon(Resource.Drawable.watermelon)
                                      .SetSound(alarmUri)
                                      .SetContentTitle("Self Tracker")
                                      .SetContentText(messageBody)
                                      .SetAutoCancel(true)
                                      .SetContentIntent(pendingIntent);

            var notificationManager = NotificationManagerCompat.From(this);
            notificationManager.Notify(MainActivity.NOTIFICATION_ID, notificationBuilder.Build());
        }
    }
}