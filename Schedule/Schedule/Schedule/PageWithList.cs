using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Schedule
{
    class PageWithList : ContentPage
    {
        private ListView lv;
        public PageWithList(Lesson[] lessons, string title)
        {
            DateTime day = Convert.ToDateTime(title);
            Title = day.ToString("ddd");
            StackLayout sl = new StackLayout() { Spacing=20};
            Label date = new Label()
            {
                Text = day.ToString("dd MMMM"),
                FontAttributes=FontAttributes.Bold,
                HorizontalOptions=LayoutOptions.Center,
                HorizontalTextAlignment=TextAlignment.Center,
                FontSize=20,
                TextColor=Color.Accent,
            };
            lv = new ListView()
            {
                ItemTemplate = new DataTemplate(() =>
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
                ,
                ItemsSource = lessons,
                HasUnevenRows = true,
                SeparatorColor = Color.Blue,
                IsPullToRefreshEnabled = false,
            };
            lv.ItemTapped += Lv_ItemTapped;
            sl.Children.Add(date);
            sl.Children.Add(lv);
            Content = sl;
        }

        private void Lv_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            lv.SelectedItem = null;
        }
    }
}