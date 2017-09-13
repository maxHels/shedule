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
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            lastChecked.Text += "\n" + DependencyService.Get<IScheduleSaver>().LoadSavedObject<DateTime>("LastUpdated.date").ToString();
            if (DependencyService.Get<IScheduleSaver>().Exist("refreshingPeriod.dat"))
            {
                TimeSpan tp = DependencyService.Get<IScheduleSaver>().LoadSavedObject<TimeSpan>("refreshingPeriod.dat");
                modyfyingMinutes.Text = tp.Minutes.ToString();
                modifyingHours.Text = tp.Hours.ToString();
            }
            else
            {
                modifyingHours.Text = "4";
                modyfyingMinutes.Text = "00";
            }
            if (DependencyService.Get<IScheduleSaver>().Exist("daysCount.txt"))
                daysCount.Text = DependencyService.Get<IScheduleSaver>().LoadText("daysCount.txt");
            else
                daysCount.Text = "8";
        }


        private void SavingButton_Clicked(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(modifyingHours.Text) && !String.IsNullOrEmpty(modyfyingMinutes.Text))
            {
                TimeSpan timeToRefresh = new TimeSpan(int.Parse(modifyingHours.Text), int.Parse(modyfyingMinutes.Text), 0);
                DependencyService.Get<IScheduleSaver>().SaveObject("refreshingPeriod.dat", timeToRefresh);
            }
            else
            {
                DisplayAlert("Ошибка!", "Необходимо заполнить часы и минуты.", "OK");
            }
            if (!String.IsNullOrEmpty(daysCount.Text)&&Convert.ToInt16(daysCount.Text)>1)
            {
                DependencyService.Get<IScheduleSaver>().SaveText("daysCount.txt", daysCount.Text);
                Application.Current.Properties["CanLoadOld"] = false;
            }
            else
            {
                DisplayAlert("Ошибка!", "Количество дней не может быть меньше двух и не должно быть пустым.", "OK");
            }
        }
    }
}