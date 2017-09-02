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
                ToolbarItem alarmSet = new ToolbarItem() //добавляем в тулбар будильник
                {
                    Icon = "clock.png",
                };
                alarmSet.Clicked += AlarmSet;
                ToolbarItems.Add(alarmSet);


                if (DependencyService.Get<IScheduleSaver>().Exist("schedule.sch") //если расписание загружалось меньше часа назад и группа не была изменена
                    && Application.Current.Properties.ContainsKey("LastGroupChoice")//, то считываем сохраненное расписание из файла
                    && (string)Application.Current.Properties["LastGroupChoice"] == DependencyService.Get<IScheduleSaver>().
                    LoadText("groupURL.txt")&&
                    DependencyService.Get<IScheduleSaver>().Exist("LastUpdated.date"))
                {
                    TimeSpan tp = new TimeSpan(1, 0, 0);
                    DateTime oldtime = DependencyService.Get<IScheduleSaver>().LoadSavedObject<DateTime>("LastUpdated.date");
                    DateTime thistime = DateTime.Now;
                    if (tp >= oldtime - thistime)
                        GetFromMemory();
                    else
                        GetFromServer();
                }
                else //в других случаях загружаем с сервера
                {
                    GetFromServer();
                }
            }
            catch //если что-то пошло не так показываем сообщение о том, что нет доступного расписания
            {
                NoSchedule();
            }
        }

        private void AlarmSet(object sender, EventArgs e) //нажатие на будильник
        {
            DateTime dayForAlarm = DateTime.Parse(this.CurrentPage.Title);
            DependencyService.Get<IAlarm>().SetAlarm((int)dayForAlarm.DayOfWeek);
        }

        private void GetFromServer()
        {
            GroupScheduler scheduler = new GroupScheduler();
            string finalURL = DependencyService.Get<IScheduleSaver>().LoadText("groupURL.txt");
            Application.Current.Properties["LastGroupChoice"] = finalURL;
            finalURL = scheduler.MakeGroupURL(finalURL);
            GroupSchedule schedule = scheduler.GetSchedule(finalURL);
            if (schedule.count != 0) //если вернулся хоть один день
            {
                FillThePage(schedule);
                DependencyService.Get<IScheduleSaver>().SaveObject<DateTime>("LastUpdated.date", DateTime.Now);
                DependencyService.Get<IScheduleSaver>().SaveObject("schedule.sch", schedule);
            }
            else //если нет расписания
            {
                NoSchedule();
            }
        }

        private void GetFromMemory()
        {
            GroupSchedule schedule = DependencyService.Get<IScheduleSaver>().LoadSavedObject<GroupSchedule>("schedule.sch");
            if (schedule.count != 0) //если сохранен хоть один день
                FillThePage(schedule);
            else //если нет расписания
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
                    Children.Add(new PageWithList(d.lessons, d.date.Replace("-", ".")));
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