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
	public partial class MenuPage : MasterDetailPage
	{
		public MenuPage ()
		{
            var masterPage = new MasterPage()
            {
                Title="Меню",
            };
            Master = masterPage;
            if (Application.Current.Properties.ContainsKey("GroupURL")) //если приложение запускается не первый раз
            {   
                Title = "Расписание";
                Detail = new ScheduleViewingPage();
            }
            else
            {
                Title = "Выбор группы";
                Detail = new FirstLaunchPage();
            }
            masterPage.ListView.ItemTapped += OnItemTapped;
			InitializeComponent();
		}

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as MasterPageItem;
            Title = item.Title;
            Detail = (Page)Activator.CreateInstance(item.TargetType); 
            IsPresented = false;

        }
    }
}