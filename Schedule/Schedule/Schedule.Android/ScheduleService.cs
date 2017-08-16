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
        private string URL;
        private GroupSchedule schedule;
        string oldJSON;
        Context context = Forms.Context;
        
        public GroupSchedule Schedule { get => schedule; }

        public override IBinder OnBind(Intent intent)
        {
            throw new NotImplementedException();
        }

        public override void OnCreate()
        {
            base.OnCreate();    
        }

        

        public GroupSchedule GetGroupSchedule(string URL)
        {
            //OnCreate();
            RequestSender requestSender = new RequestSender();
            JSONParser parser = new JSONParser();
            schedule = parser.JSONToObjectParsing<GroupSchedule>(requestSender.sendRequest(URL));
            //Start(URL);
            return schedule;
        }

        private bool scheduleComparing(string JSON)
        {
            if (JSON != oldJSON)
            {
                oldJSON = JSON;
                return false;
            }
            else
                return true;
        }

        private void Notificator()
        {
            //if (!scheduleComparing(new RequestSender().sendRequest(URL)))
            //{
                NotificationCompat.Builder builder = new NotificationCompat.Builder(context);
                builder.SetSmallIcon(Droid.Resource.Drawable.notification_template_icon_bg).
                    SetContentTitle("Расписание обновлено!").SetContentText("Добавлены новые пары!");
                NotificationManager manager =(NotificationManager)context.GetSystemService(Service.NotificationService);
                Notification not = builder.Build();
                manager.Notify();
            //}
        }

        public void Start(string URL)
        {
            this.URL = URL;
            Timer timer = new Timer(15000);
            timer.Elapsed += Timer_Elapsed;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Notificator();
        }
    }
}