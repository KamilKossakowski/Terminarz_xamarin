using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.App.Job;
using System.Collections.Generic;
using System.Linq;

namespace Terminarzkk.Droid
{
    [Activity(Label = "Terminarzkk", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        

        Button stopServiceButton;
        Button startServiceButton;
        Intent startServiceIntent;
        Intent stopServiceIntent;
        bool isStarted = false;
        public const string IntentValueKey = "userValue";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            var intent = new Intent(this, typeof(ForegroundServise));
            // Can use intent to pass data to the service.
            intent.PutExtra(IntentValueKey, $"Foreground Service ");

            // This method is async and return immediately.
           // this.StartService(intent);
           // var javaClass = Java.Lang.Class.FromType(typeof(Tlo));
           /// var compName = new ComponentName(this, javaClass);
           // var jobInfo = new JobInfo.Builder(0, jobService: compName)
           //    .SetPeriodic(3600000)
           //    .SetPersisted(true)
           //    .Build();
            
        }
        
       
    }
}