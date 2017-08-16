using Xamarin.Forms;

namespace Schedule
{
    class ScheduleCell:ViewCell
    {
        Label title,descr;
        public ScheduleCell()
        {
            title = new Label();
            descr = new Label();
            StackLayout cell = new StackLayout();
            cell.Children.Add(title);
            cell.Children.Add(descr)
        }
    }
}