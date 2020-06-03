using System;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.App.Job;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Support.V4.App;
using Android.Util;
namespace Terminarzkk.Droid
{
    [Service (Name = "com.companyname.Terminarzkk.tlo",Permission ="android.permission.BIND_JOB_SERVICE")]
    class Tlo : JobService
    {   Wysylka w = new Wysylka();
        public override bool OnStartJob(JobParameters tloparams)
        {

            Task.Run(() =>
            {
                Console.WriteLine("1hhhh");
                w.Czywyslc();
                Log.Debug("moje zadanie w tle", "to działa...chyba");
                Console.WriteLine("2hhhh");
            });
            return true;
        }
       
        public override bool OnStopJob(JobParameters tloparams)
        {
            return true;
        }
    }
}