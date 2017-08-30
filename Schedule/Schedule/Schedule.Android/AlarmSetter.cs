using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Schedule.Droid;
using Android.Provider;
using Java.Util;
using Java.Lang;

[assembly:Dependency(typeof(AlarmSetter))]
namespace Schedule.Droid
{
    class AlarmSetter : IAlarm
    {
        public void SetAlarm()
        {
            Context c = Forms.Context;
            Intent i = new Intent(AlarmClock.ActionSetAlarm);
            c.StartActivity(i);
        }

        public void SetAlarm(int dayOfWeek)
        {
            Context c = Forms.Context;
            Intent i = new Intent(AlarmClock.ActionSetAlarm);
            ArrayList alarmDays = new ArrayList();
            alarmDays.Add(dayOfWeek + 1);
            i.PutExtra(AlarmClock.ExtraDays,alarmDays);
            c.StartActivity(i);
        }
    }
}