using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Schedule
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            if (!Current.Properties.ContainsKey("GroupURL"))
                MainPage = new NavigationPage(new FirstLaunchPage());
            else
                MainPage = new NavigationPage(new ScheduleViewingPage((string)Current.Properties["GroupURL"]));
        }

		protected override void OnStart ()
		{
            // Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
