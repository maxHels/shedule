using System.Collections.Generic;
using Xamarin.Forms;

namespace Schedule
{
    class PageWithList : ContentPage
    {
        
        public PageWithList(Lesson[] lessons, string title)
        {
            Title = title;
            ListView lv = new ListView() { ItemTemplate = new DataTemplate(() =>
             {
                 ScheduleCell sch = new ScheduleCell(Title) { };
                 sch.SetBinding(ScheduleCell.TitleProperty, "title");
                 sch.SetBinding(ScheduleCell.DescrPreperty, "type");
                 sch.SetBinding(ScheduleCell.StartTimeProperty, "timeStart");
                 sch.SetBinding(ScheduleCell.FinishTimeProperty, "timeEnd");
                 sch.SetBinding(ScheduleCell.TeacherProperty, "teacher.fullname");
                 sch.SetBinding(ScheduleCell.AddressProperty, "address");
                 sch.SetBinding(ScheduleCell.RoomProperty, "room");
                 sch.SetBinding(ScheduleCell.SubgroupProperty, "subgroup.title");
                 return sch;
             })
                , ItemsSource = lessons, HasUnevenRows = true, SeparatorColor=Color.Blue, IsPullToRefreshEnabled=true, };
            lv.Refreshing += ScheduleFefreshing;
            Content = lv;
        }

        private void ScheduleFefreshing(object sender, System.EventArgs e)
        {
            
        }
    }
}