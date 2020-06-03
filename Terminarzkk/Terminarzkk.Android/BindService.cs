using System;
using Android.App;
using Android.OS;
using Android.Content;
using System.Threading;
using System.Threading.Tasks;
using Android.Widget;

namespace ServicesTest
{
	/// <summary>
	/// Gets access to the service. Stores a strong reference to the service.
	/// </summary>
	public class TestBinder : Binder
	{
		public TestBinder (TestBindService service)
		{
			this.Service = service;			
		}

		public TestBindService Service
		{
			get;
		} 
	}

	public class TestServiceConnection : Java.Lang.Object, IServiceConnection
	{
		public TestBinder Binder
		{
			get;
			private set;
		}

		public event EventHandler<bool> ConnectionChanged;

		public void OnServiceConnected (ComponentName name, IBinder service)
		{
			this.Binder = (TestBinder)service;
			this.ConnectionChanged?.Invoke(this, true);
		}

		public void OnServiceDisconnected (ComponentName name)
		{
			this.Binder = null;
			this.ConnectionChanged?.Invoke(this, false);
		}
	}

	/// <summary>
	/// The Service itself will be there only once. Sending multiple intents will not creae multiple instances.
	/// </summary>
	[Service]
	public class TestBindService : Service
	{
		public const string IntentValueKey = "UserValue";

		#region implemented abstract members of Service

		public override IBinder OnBind (Intent intent)
		{
			MainActivity.L("OnBind() called");
			this.DoAsyncWork(intent);
			return new TestBinder(this);
		}

		/// <summary>
		/// Public property which we will be able to access through the binder.
		/// </summary>
		/// <value>The current progress.</value>
		public int CurrentProgress
		{
			get;
			private set;
		}

		void DoAsyncWork (Intent intent)
		{
			
			// Can use intent to pass data to the service.
			var val = intent.GetStringExtra(IntentValueKey);

			Toast.MakeText(this, $"OnStartCommand({val})", ToastLength.Long).Show();	

			// Regular Service (unlike IntentService) runs on the main thread so we must take care of threading here.
			Task.Run(() => {
				for(int i = 1; i < 999 && !this.cts.IsCancellationRequested; i++)
				{
					this.CurrentProgress = i;
					MainActivity.L($"Processing...{i}");
					Thread.Sleep(1000);
				}

				// Stop when done. There is an override that takes an integer parameter. That's the start ID. The service will then only stop
				// if the current start ID matches the one passed to StopSelf(). This prevents stopping the service internally if there are still Intents pending with
				// a higher start ID. (Again: stopping stops the ENTIRE service, not just this current workload!)
				if(!this.cts.IsCancellationRequested)
				{
					this.StopSelf();
				}
			});
						
		}

		/// <summary>
		/// This will be called once for every intent.
		/// </summary>
		/// <param name="intent">Intent.</param>
		/// <param name="flags">Flags.</param>
		/// <param name="startId">Start identifier.</param>
		public override StartCommandResult OnStartCommand (Intent intent, StartCommandFlags flags, int startId)
		{
			// This will only be called if the service also is started by the app.
			this.DoAsyncWork(intent);

			// Sticky:
			// This mode makes sense for things that will be explicitly started and stopped to run for arbitrary periods of time, such as a service performing background music playback.
			// http://developer.android.com/reference/android/app/Service.html#START_STICKY

			// Redeliver Intent:
			// The service will not receive a onStartCommand(Intent, int, int) call with a null Intent because it will will only be re-started if it is not finished processing all Intents sent to it
			// (and any such pending events will be delivered at the point of restart).
			// http://developer.android.com/reference/android/app/Service.html#START_REDELIVER_INTENT

			// Not sticky:
			// This mode makes sense for things that want to do some work as a result of being started, but can be stopped when under memory pressure and will explicit start themselves again later to do more work.
			// An example of such a service would be one that polls for data from a server: it could schedule an alarm to poll every N minutes by having the alarm start its service.
			// When its onStartCommand(Intent, int, int) is called from the alarm, it schedules a new alarm for N minutes later, and spawns a thread to do its networking. If its process is killed while doing that check, the service will not be restarted until the alarm goes off.
			// http://developer.android.com/reference/android/app/Service.html#START_NOT_STICKY

			return StartCommandResult.Sticky;
		}

		#endregion


		CancellationTokenSource cts = new CancellationTokenSource();

		public override void OnDestroy ()
		{
			MainActivity.L("Bound service has been destroyed.");
			// If the service is cancelled (e.g. by calling StopService()), stop the threads we created.
			cts.Cancel();
			base.OnDestroy ();
		}
	}
}

