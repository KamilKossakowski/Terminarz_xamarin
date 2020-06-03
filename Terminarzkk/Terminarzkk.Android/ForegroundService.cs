using System;
using Android.App;
using Android.Util;
using Android.Content;
using Android.OS;
using Android.Widget;
using System.Threading.Tasks;
using System.Threading;

namespace Terminarzkk.Droid
{

   
    [Service]
    public class ForegroundServise : Service
    {
        static readonly string TAG = typeof(MainActivity).FullName;
        public const string IntentValueKey = "UserValue";
        #region implemented abstract members of Service
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            var notification = new Notification(Resource.Drawable.icon, "Service running in the foreground!");
            var notificationIntent = new Intent(this, typeof(MainActivity));
            var pendingIntent = PendingIntent.GetActivity(this, 0, notificationIntent, 0);
            notification.SetLatestEventInfo(this, "Foreground Service", "Tap to to to App", pendingIntent);
            Log.Info(TAG, "OnStartCommand: The service is starting.");
            StartForeground((int)NotificationFlags.ForegroundService, notification);
            Task.Run(() => {
                for (int i = 1; i < 999; i++)
                {
                    Wysylka w = new Wysylka();
                    w.Czywyslc();
                    Thread.Sleep(3600000);
                if (!this.cts.IsCancellationRequested)
                {
                    this.StopSelf();
                }
                    i--;
                }
            });
            return StartCommandResult.Sticky;

        }
        #endregion

        CancellationTokenSource cts = new CancellationTokenSource();




        public override void OnDestroy()
        {

            cts.Cancel();
            base.OnDestroy();
        }


    }
}