using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Schedule
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScheduleViewingPage : TabbedPage
    {
        GroupSchedule schedule;
        public ScheduleViewingPage (string URL)
        {
            InitializeComponent();
            schedule = new GroupScheduler().GetSchedule(URL);
            foreach (Day d in schedule.days)
            {
                Children.Add(new PageWithList(d.lessons, d.date));
            }
        }

        
    }
}