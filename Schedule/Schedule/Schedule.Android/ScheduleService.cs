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
    [Service]
    class ScheduleService : Service, IService
    {
        Context context;
        Timer timer;
        GroupSchedule oldSchedule, newSchedule;
        

        public override IBinder OnBind(Intent intent)
        {
            throw new NotImplementedException();
        }

        //[return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, /*[GeneratedEnum]*/ StartCommandFlags flags, int startId)
        {
            Start();
            return StartCommandResult.Sticky;
        }

        public override void OnDestroy()
        {
            timer.Dispose();
            base.OnDestroy();
        }

        private void Notificator()
        {
            context = this;
            if (context != null)
            {
                NotificationCompat.Builder builder = new NotificationCompat.Builder(context);
                builder.SetSmallIcon(Droid.Resource.Drawable.Icon).
                    SetContentTitle("Расписание обновлено!").SetContentText("Добавлены новые пары!").SetVibrate(new long[] { 1000, 1000 });
                NotificationManager manager = (NotificationManager)context.GetSystemService(Service.NotificationService);
                Notification not = builder.Build();
                manager.Notify(0, not);
            }
        }

        public void Start()
        {
            timer = new Timer(7000);
            timer.Start();
            timer.AutoReset = true;
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Notificator();
        }

        public GroupSchedule GetGroupSchedule(string URL)
        {
            throw new NotImplementedException();
        }
    }
}