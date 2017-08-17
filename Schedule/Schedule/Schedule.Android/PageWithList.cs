using Xamarin.Forms;

namespace Schedule
{
    internal class PageWithList : ContentPage
    {
        private Lesson[] lessons;
        private string v;

        public PageWithList(Lesson[] lessons, string title)
        {
            this.lessons = lessons;
            Title = title;
            ListView lv = new ListView() { ItemTemplate = new DataTemplate(() =>
             {
                 ScheduleCell sch = new ScheduleCell { };
                 sch.SetBinding(ScheduleCell.TitleProperty, "title");
                 sch.SetBinding(ScheduleCell.DescrPreperty, "type");
                 sch.SetBinding(ScheduleCell.StartTimeProperty, "timeStart");
                 sch.SetBinding(ScheduleCell.FinishTimeProperty, "timeEnd");
                 sch.SetBinding(ScheduleCell.TeacherProperty, "teacher.fullname");
                 sch.SetBinding(ScheduleCell.AddressProperty, "address");
                 sch.SetBinding(ScheduleCell.RoomProperty, "room");
                 return sch;
             })
                , ItemsSource = lessons, HasUnevenRows = true };
            Content = lv;
        } 
    }
}