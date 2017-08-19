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
using Android;

namespace Schedule.Droid
{
    [BroadcastReceiver(Enabled = true,Exported = true)]
    //[IntentFilter(new[] { Intent.ActionBootCompleted})]
    public class ReBootReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            context.StartService(new Intent(context, typeof(ScheduleService)));
        }
    }
}