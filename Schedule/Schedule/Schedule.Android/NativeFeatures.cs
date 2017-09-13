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

[assembly :Xamarin.Forms.Dependency(typeof(NativeFeatures))]
namespace Schedule.Droid
{
    class NativeFeatures : INative
    {
        Context c;
        public void ShowText(string text)
        {
            c = Forms.Context;
            Toast.MakeText(c, text, ToastLength.Long).Show();
        }
    }
}