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
    [Service(Enabled = true)]
    class ScheduleService : Service
    {
        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            CheckSchedule();
            Start();
            return StartCommandResult.Sticky;
        }

        private void Start()
        {
            Timer timer = new Timer(60000)
            {
                AutoReset = false,
            };
            timer.Elapsed += TimeToCheck;
            timer.Start();
        }

        private void TimeToCheck(object sender, ElapsedEventArgs e)
        {
            CheckSchedule();
        }

        private void notWithSomeText(int id, string text)
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
                int i = 0;
                TimeSpan interval;
                if (saver.Exist("refreshingPeriod.dat"))
                    interval = saver.LoadSavedObject<TimeSpan>("refreshingPeriod.dat");
                else
                    interval = new TimeSpan(4, 0, 0);
                notWithSomeText(i, interval.ToString());
                i++;
                DateTime lastChecked = saver.LoadSavedObject<DateTime>("LastUpdated.date");
                notWithSomeText(i, "last check:" + lastChecked.ToString());
                i++;
                DateTime time = DateTime.Now;
                notWithSomeText(i, "time:" + time.ToString());
                i++;
                TimeSpan tp = time - lastChecked;
                notWithSomeText(i, "tp:" + tp.ToString());
                i++;
                if (TimeSpan.Compare(interval,tp)==-1)
                {
                    notWithSomeText(i, "if:");
                    i++;
                    GroupScheduler scheduler = new GroupScheduler();
                    notWithSomeText(i, "SchedulerCreated");
                    i++;
                    List<Lesson> addedLessons = new List<Lesson>();
                    notWithSomeText(i, "Empty list initialized");
                    i++;
                    string URL = scheduler.MakeGroupURL(saver.LoadText("groupURL.txt"));
                    notWithSomeText(i, URL);
                    i++;
                    GroupSchedule oldSchedule = saver.LoadSavedObject<GroupSchedule>("schedule.sch");
                    notWithSomeText(i, "old schedule count:" + oldSchedule.count.ToString());
                    i++;
                    GroupSchedule refreshedSchedule = scheduler.GetSchedule(URL);
                    notWithSomeText(i, "new schedule count:" + refreshedSchedule.count.ToString());
                    i++;
                    addedLessons = new ScheduleComparer().AddedLessons(oldSchedule, refreshedSchedule);
                    notWithSomeText(i, "Compared!" + addedLessons.Count.ToString());
                    i++;
                    if (addedLessons != null)
                    {
                        notWithSomeText(i, "in second if:");
                        i++;
                        saver.SaveObject("LastUpdated.date", DateTime.Now);
                        notWithSomeText(i, "saved date");
                        i++;
                        saver.SaveObject("schedule.sch", refreshedSchedule);
                        notWithSomeText(i, "saved schedule!");
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