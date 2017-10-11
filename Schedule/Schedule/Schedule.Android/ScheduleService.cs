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
    [Service(Enabled =true)]
    class ScheduleService : Service
    {
        [return:Android.Runtime.GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            Start();
            return StartCommandResult.Sticky;
        }
        
        

        private void Start()
        {
            Timer timer = new Timer(60000)
            {
                AutoReset = true,
            };
            timer.Elapsed += TimeToCheck;
            timer.Start();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            NotWithSomeText(1, "Service destroyed");
        }

        private void TimeToCheck(object sender, ElapsedEventArgs e)
        {
            CheckSchedule();
        }

        private void NotWithSomeText(int id, string text)
        {
            Notification n = new Notification.Builder(this)
                        .SetContentTitle(text)
                        .SetSmallIcon(Resource.Drawable.Icon)
                        .SetAutoCancel(false)
                        .Build();
            NotificationManager nm = (NotificationManager)GetSystemService(NotificationService);
            nm.Notify(id, n);
        }

        private void CheckSchedule()
        {
            ScheduleSaver saver = new ScheduleSaver();
            try
            {
                TimeSpan interval;
                if (saver.Exist("refreshingPeriod.dat"))
                    interval = saver.LoadSavedObject<TimeSpan>("refreshingPeriod.dat");
                else
                    interval = new TimeSpan(4, 0, 0);
                DateTime lastChecked = saver.LoadSavedObject<DateTime>("LastUpdated.date");
                DateTime time = DateTime.Now;
                TimeSpan tp = time - lastChecked;
                if (TimeSpan.Compare(interval,tp)==-1)
                {
                    GroupScheduler scheduler = new GroupScheduler();
                    List<Lesson> addedLessons = new List<Lesson>();
                    string URL = scheduler.MakeGroupURL(saver.LoadText("groupURL.txt"),saver);
                    GroupSchedule oldSchedule = saver.LoadSavedObject<GroupSchedule>("schedule.sch");
                    GroupSchedule refreshedSchedule = scheduler.GetSchedule(URL);
                    addedLessons = new ScheduleComparer().AddedLessons(oldSchedule, refreshedSchedule);
                    if (addedLessons != null)
                    {
                        saver.SaveObject("LastUpdated.date", DateTime.Now);
                        saver.SaveObject("schedule.sch", refreshedSchedule);
                        NotificateAboutChanges(addedLessons);
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            }
            catch
            {
                if (saver.Exist("LastUpdated.date"))
                {
                    DateTime lastChecked = saver.LoadSavedObject<DateTime>("LastUpdated.date");
                    Notification n = new Notification.Builder(this)
                        .SetContentTitle("Не удалось обновить расписание!")
                        .SetSmallIcon(Resource.Drawable.Icon)
                        .SetContentText("Последний раз обновлено " + lastChecked.ToString())
                        .SetAutoCancel(true)
                        .SetVibrate(new long[] { 1000, 1000, 1000 })
                        .Build();
                    NotificationManager nm = (NotificationManager)GetSystemService(NotificationService);
                    nm.Notify(0, n);
                }
                else
                {
                    Notification n = new Notification.Builder(this)
                        .SetContentTitle("Не удалось обновить расписание!")
                        .SetSmallIcon(Resource.Drawable.Icon)
                        .SetAutoCancel(true)
                        .SetVibrate(new long[] { 1000, 1000, 1000 })
                        .Build();
                    NotificationManager nm = (NotificationManager)GetSystemService(NotificationService);
                    nm.Notify(50, n);
                }
            }
        }

        private void NotificateAboutChanges(List<Lesson> addedLessons)
        {
            Notification.Builder b = new Notification.Builder(this);
            NotificationManager manager = (NotificationManager)GetSystemService(NotificationService);
            if (addedLessons.Count!=0)
            {
                if (addedLessons.Count >= 4)
                {
                    string summary = "и ещё " + (addedLessons.Count - 4).ToString() + " пары";
                    Notification notification = b
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
                             .SetAutoCancel(true)
                             .SetVibrate(new long[] { 1000, 1000, 1000 })
                        .Build();
                    manager.Notify(0, notification);
                }
                else
                {
                    if (addedLessons.Count < 4 && addedLessons.Count > 0)
                    {
                        if (addedLessons.Count == 1)
                        {
                            Notification notification = b
                                .SetContentTitle("Расписание обновлено")
                                .SetContentText(addedLessons.ElementAt(0).title)
                                .SetSmallIcon(Resource.Drawable.Icon)
                                .SetVibrate(new long[] { 1000, 1000, 1000 })
                                .SetAutoCancel(true)
                                .Build();
                            manager.Notify(0, notification);
                        }
                        
                        if (addedLessons.Count == 3)
                        {
                            Notification notification = b
                                .SetContentTitle("Расписание обновлено")
                                .SetSmallIcon(Resource.Drawable.Icon)
                                .SetContentText(addedLessons.ElementAt(0).title+"\n"
                                + addedLessons.ElementAt(1).title + "\n"
                                + addedLessons.ElementAt(2).title)
                                .SetVibrate(new long[] { 1000, 1000, 1000 })
                                .SetAutoCancel(true)
                                .Build();
                            manager.Notify(0, notification);
                        }
                        if(addedLessons.Count==2)
                        {
                            Notification notification = b
                                .SetContentTitle("Расписание обновлено")
                                .SetSmallIcon(Resource.Drawable.Icon)
                                .SetContentText(addedLessons.ElementAt(0).title + "\n"
                                + addedLessons.ElementAt(1).title)
                                .SetVibrate(new long[] { 1000, 1000, 1000 })
                                .SetAutoCancel(true)
                                .Build();
                            manager.Notify(0, notification);
                        }
                    }
                }
            }
            else
            {
                Notification notification = b
                             .SetContentTitle("Расписание обновлено")
                             .SetContentText("Пар добавлено не было")
                             .SetSmallIcon(Resource.Drawable.Icon)
                             .SetVibrate(new long[] { 1000, 1000, 1000 })
                             .SetAutoCancel(true).
                             Build();
                manager.Notify(0, notification);
            }
        }

        public override IBinder OnBind(Intent intent)
        {
            throw new NotImplementedException();
        }

    }
}