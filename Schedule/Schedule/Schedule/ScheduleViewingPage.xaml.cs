using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Schedule
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScheduleViewingPage : TabbedPage
    {
        public ScheduleViewingPage()
        {
            InitializeComponent();
            try
            {
                ToolbarItem alarmSet = new ToolbarItem()
                {
                    Icon="clock.png",
                };
                alarmSet.Clicked += AlarmSet;
                ToolbarItems.Add(alarmSet);
                TimeSpan tp = new TimeSpan(1, 0, 0);
                DateTime oldtime = (DateTime)Application.Current.Properties["LastUpdated"];
                DateTime thistime = DateTime.Now;
                if (thistime - oldtime <= tp &&
                    DependencyService.Get<IScheduleSaver>().ExistAsync("schedule.sch")
                    && Application.Current.Properties.ContainsKey("LastGroupChoice")
                    && (string)Application.Current.Properties["LastGroupChoice"] == (string)Application.Current.Properties["GroupURL"])
                {
                    GetFromMemory();
                }
                else
                {
                    GetFromServer();
                }
            }
            catch
            {
                NoSchedule();
            }
        }

        private void AlarmSet(object sender, EventArgs e)
        {
            DateTime dayForAlarm = DateTime.Parse(this.CurrentPage.Title);
            DependencyService.Get<IAlarm>().SetAlarm((int)dayForAlarm.DayOfWeek);
        }

        private void GetFromServer()
        {
            GroupScheduler scheduler = new GroupScheduler();
            string finalURL = (string)Application.Current.Properties["GroupURL"];
            Application.Current.Properties["LastGroupChoice"] = finalURL;
            finalURL = scheduler.MakeGroupURL(finalURL);
            GroupSchedule schedule = scheduler.GetSchedule(finalURL);
            if (schedule.count != 0)
            {
                FillThePage(schedule);
                Application.Current.Properties["LastUpdated"] = DateTime.Now;
                DependencyService.Get<IScheduleSaver>().SaveScheduleAsync("schedule.sch", schedule);
            }
            else
            {
                NoSchedule();
            }
        }

        private void GetFromMemory()
        {
            GroupSchedule schedule = DependencyService.Get<IScheduleSaver>().LoadScheduleAsync("schedule.sch");
            if (schedule.count != 0)
                FillThePage(schedule);
            else
                NoSchedule();
        }

        private void NoSchedule()
        {
            StackLayout sl = new StackLayout();
            sl.Children.Add(new Label()
            {
                Text = "Нет доступного расписания.",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            });
            Children.Add(new ContentPage()
            {
                Content = sl,
            });
        }

        private void FillThePage(GroupSchedule schedule)
        {
            if (schedule != null)
                foreach (Day d in schedule.days)
                {
                    Children.Add(new PageWithList(d.lessons, d.date.Replace("-",".")));
                }
            else
                throw new Exception();
        }
    }
}


//StackLayout slay = new StackLayout();
//ActivityIndicator indicator = new ActivityIndicator()
//{
//    VerticalOptions = LayoutOptions.Center,
//    HorizontalOptions=LayoutOptions.Center,
//};
//slay.Children.Add(indicator);
//var indicatorPage = new ContentPage()
//{
//    Content = slay,
//};
//Children.Add(indicatorPage);
//InvalidateMeasure();
//GroupSchedule schedule = new GroupScheduler().GetSchedule(finalURL);
//Children.Remove(indicatorPage);