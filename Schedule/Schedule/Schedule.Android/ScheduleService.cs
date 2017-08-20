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
                    GroupScheduler scheduler = new GroupScheduler();
                    List<Lesson> addedLessons = scheduler.EditedLessons(scheduler.
                        GetSchedule("http://api.grsu.by/1.x/app2/getGroupSchedule?groupId=5146&dateStart=21.02.2017&dateEnd=26.02.2017"),
                        scheduler.GetSchedule("http://api.grsu.by/1.x/app2/getGroupSchedule?groupId=5146&dateStart=23.02.2017&dateEnd=01.03.2017"));
                    string summary = "and " + (addedLessons.Count - 2).ToString() + " more";
                    Notification notification = new Notification.Builder(context)
                         .SetContentTitle("Расписание обновлено")
                         .SetContentText("Добавлены пары")
                         .SetSmallIcon(Resource.Drawable.Icon)
                         .SetStyle(new Notification.InboxStyle()
                             .AddLine(addedLessons.ElementAt(0).title)
                             .AddLine(addedLessons.ElementAt(1).title)
                             .SetBigContentTitle("")
                             .SetSummaryText("+3 more"))
                         .Build();
                    NotificationManager manager = (NotificationManager)context.GetSystemService(Service.NotificationService);
                    Notification not = builder.Build();
                    manager.Notify(0, notification);
            }
        }

        public void Start()
        {
            timer = new Timer(15000);
            timer.Start();
            timer.AutoReset = false;
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