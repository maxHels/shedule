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

        public override IBinder OnBind(Intent intent)
        {
            throw new NotImplementedException();
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            context = this;
            Checker();
            Start();
            Notification notification = new Notification.Builder(context)
                                .SetContentTitle("Расписание обновлено")
                                .SetContentTitle("Сервис запущен")
                                .SetSmallIcon(Resource.Drawable.Icon)
                                .Build();
            NotificationManager m = (NotificationManager)context.GetSystemService(Service.NotificationService);
            m.Notify(0, notification);
            return StartCommandResult.Sticky;
        }

        private void Checker()
        {
            if (App.Current.Properties.ContainsKey("LastUpdated"))
            {
                TimeSpan interval = new TimeSpan(4, 0, 0);
                TimeSpan difference = DateTime.Now.Subtract((DateTime)App.Current.Properties["LastUpdated"]);
                if (difference >= interval)
                    Notificator();
            }
        }

        public override void OnDestroy()
        {
            timer.Dispose();
            base.OnDestroy();
        }

        private void Notificator()
        {
            context = this;

            if (DependencyService.Get<IScheduleSaver>().ExistAsync("schedule.sch"))
            {
                GroupScheduler scheduler = new GroupScheduler();
                List<Lesson> addedLessons = new List<Lesson>();
                GroupSchedule oldSchedule = DependencyService.Get<IScheduleSaver>().LoadScheduleAsync("schedule.sch");
                string URL = scheduler.MakeGroupURL((string)App.Current.Properties["GroupURL"]);
                GroupSchedule refreshedSchedule = scheduler.GetSchedule(URL);
                addedLessons = scheduler.EditedLessons(oldSchedule, refreshedSchedule);
                App.Current.Properties["LastUpdated"] = DateTime.Now;

                if (addedLessons.Count != 0)
                {
                    DependencyService.Get<IScheduleSaver>().SaveScheduleAsync("schedule.sch", refreshedSchedule);
                    if (addedLessons.Count >= 3)
                    {
                        string summary = "и ещё "+(addedLessons.Count - 4).ToString()+" пары";
                        Notification notification = new Notification.Builder(context)
                             .SetContentTitle("Расписание обновлено")
                             .SetContentText("Добавлены пары")
                             .SetSmallIcon(Resource.Drawable.Icon)
                             .SetStyle(new Notification.InboxStyle()
                                 .AddLine(addedLessons.ElementAt(0).title)
                                 .AddLine(addedLessons.ElementAt(1).title)
                                 .AddLine(addedLessons.ElementAt(2).title)
                                 .AddLine(addedLessons.ElementAt(3).title)
                                 .SetBigContentTitle("")
                                 .SetSummaryText(summary))
                             .Build();
                        NotificationManager manager = (NotificationManager)context.GetSystemService(Service.NotificationService);
                        manager.Notify(0, notification);
                    }
                    if (addedLessons.Count < 3 && addedLessons.Count > 0)
                    {
                        NotificationManager m = NotificationManager.FromContext(context);
                        if (addedLessons.Count == 1)
                        {
                            Notification notification = new Notification.Builder(context)
                                .SetContentTitle("Расписание обновлено")
                                .SetContentTitle(addedLessons.ElementAt(0).title)
                                .SetSmallIcon(Resource.Drawable.Icon)
                                .Build();
                            m.Notify(0, notification);
                        }
                        else
                        {
                            Notification notification = new Notification.Builder(context)
                                .SetContentTitle("Расписание обновлено")
                                .SetContentTitle(addedLessons.ElementAt(0).title + " и\n " + addedLessons.ElementAt(1).title)
                                .SetSmallIcon(Resource.Drawable.Icon)
                                .Build();
                            m.Notify(0, notification);
                        }
                    }
                }
            }
            else
            {
                Notification notification = new Notification.Builder(context)
                             .SetContentTitle("Расписание обновлено")
                             .SetContentText("Пар добавлено не было")
                             .SetSmallIcon(Resource.Drawable.Icon).Build();
                NotificationManager manager = (NotificationManager)context.GetSystemService(Service.NotificationService);
                manager.Notify(0, notification);
            }
        }

        public void Start()
        {
            timer = new Timer(180000);
            timer.Start();
            timer.AutoReset = true;
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Checker();
        }

        public GroupSchedule GetGroupSchedule(string URL)
        {
            throw new NotImplementedException();
        }
    }
}