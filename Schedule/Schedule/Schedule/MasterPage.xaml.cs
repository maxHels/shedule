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
	public partial class MasterPage : ContentPage
	{
        public ListView ListView { get { return listView; } }
		public MasterPage ()
		{
            
			InitializeComponent ();
            var masterPageItem = new List<MasterPageItem>();
            masterPageItem.Add(new MasterPageItem
            {
                Title = "Выбор группы",
                Detail = "",
                TargetType = typeof(FirstLaunchPage),
            });
            masterPageItem.Add(new MasterPageItem
            {
                Title = "Расписание",
                Detail = "",
                TargetType = typeof(ScheduleViewingPage),
            });
            masterPageItem.Add(new MasterPageItem
            {
                Title="Настройки",
                Detail="",
                TargetType=typeof(SettingsPage),
            });
            listView.BindingContext = this;
            listView.ItemsSource = masterPageItem;
            listView.ItemTemplate = new DataTemplate(() =>
              {
                  TextCell textCell = new TextCell() { TextColor = Color.FromRgb(255,64,129), };
                  textCell.SetBinding(TextCell.TextProperty, "Title");
                  return textCell;
              });
		}
    }
}