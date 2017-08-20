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
            var masterPage = new MasterPage();
            Master = masterPage;
            Title = "Menu";
            Detail = new ScheduleViewingPage();
            masterPage.ListView.ItemTapped += OnItemTapped;
			InitializeComponent();
		}

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as MasterPageItem;
            Detail = (Page)Activator.CreateInstance(item.TargetType); 
            IsPresented = false;
        }
    }
}