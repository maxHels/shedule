using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Xamarin.Forms;
using Android.Support.V4.App;

[assembly: Dependency(typeof(Schedule.Droid.ScheduleService))]
namespace Schedule.Droid
{
    [Service (Enabled = true)]
    class ScheduleService : Service
    {
        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            Start();
            return StartCommandResult.Sticky;
        }

        private void Start()
        {
            Notif();
            Timer timer = new Timer(5000);
            timer.Start();
            timer.AutoReset = true;
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Notif();
        }

        private void Notif()
        {
            string hfr = (string)App.Current.Properties["GroupURL"];
            Notification n = new Notification.Builder(this).
                SetContentTitle("jhb").SetContentText("nkdc").SetSmallIcon(Resource.Drawable.Icon).Build();
            NotificationManager nm = (NotificationManager)GetSystemService(Service.NotificationService);
            nm.Notify(0, n);
        }

        public override IBinder OnBind(Intent intent)
        {
            throw new NotImplementedException();
        }

    }
}