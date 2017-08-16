using Xamarin.Forms;

namespace Schedule
{
    internal class PageWithList : ContentPage
    {
        private Lesson[] lessons;
        private string v;

        public PageWithList(Lesson[] lessons, string v)
        {
            this.lessons = lessons;
            this.v = v;
            ListView lv= new ListView() { ItemTemplate = new DataTemplate(typeof(ScheduleCell)), ItemsSource = lessons };
            Content = lv;
        } 
    }
}