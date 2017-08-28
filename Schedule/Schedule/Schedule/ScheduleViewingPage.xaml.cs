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
        public ScheduleViewingPage (GroupSchedule schedule)
        {
            InitializeComponent();
            foreach (Day d in schedule.days)
            {
                Children.Add(new PageWithList(d.lessons, d.date));
            }
        }

    }
}