using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Schedule
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstLaunchPage : ContentPage
	{
        JSONArray faculties, departments, groups;
        GroupSchedule thisSchedule;
        List<string> Faculties, Departments, Groups;
        string finalURL, groupURL;
        RequestSender requestSender = new RequestSender();
        JSONParser parser = new JSONParser();
        ActivityIndicator i=new ActivityIndicator()
        {
            IsRunning=true,
            IsEnabled=true,
            IsVisible=false,
        };

        public FirstLaunchPage()
        {
            InitializeComponent();
            mainLayout.Children.Add(i);
            faculties = parser.JSONToObjectParsing<JSONArray>(requestSender.sendRequest("http://api.grsu.by/1.x/app1/getFaculties"));
            Faculties = parser.ToListWithoutId(faculties);
            facultyPicker.ItemsSource = Faculties;
            facultyPicker.SelectedIndexChanged += onGroupCreating;
            departmentPicker.SelectedIndexChanged += onGroupCreating;
            departments = parser.JSONToObjectParsing<JSONArray>(requestSender.sendRequest("http://api.grsu.by/1.x/app1/getDepartments"));
            Departments = parser.ToListWithoutId(departments);
            departmentPicker.ItemsSource = Departments;
            treatiseSelector.ValueChanged += onGroupCreating;
            groupPicker.SelectedIndexChanged += onFinalUrlCreation;
        }

        private void onFinalUrlCreation(object sender, EventArgs e)
        {
            if (groupPicker.SelectedItem != null)
            {
                finalURL = "http://api.grsu.by/1.x/app1/getGroupSchedule?groupId=";
                finalURL += groups.items[groupPicker.SelectedIndex].id;
                finalURL += "&dateStart=21.03.2017&dateEnd=25.03.2017";
                getSchedule.IsEnabled = true;
            }
        }

        private void onGroupCreating(object sender, EventArgs e)
        {
           
            groupCreating();
           
        }

        private void groupCreating()
        {
            groupURL = "http://api.grsu.by/1.x/app1/getGroups?departmentId=";
            if (facultyPicker.SelectedIndex != -1 && departmentPicker.SelectedIndex != -1)
            {
                
                groupURL += departments.items[departmentPicker.SelectedIndex].id;
                groupURL += "&facultyId=";
                groupURL += faculties.items[facultyPicker.SelectedIndex].id;
                groupURL += "&course=";
                groupURL += treatiseSelector.Value.ToString();
                groups = new JSONParser().JSONToObjectParsing<JSONArray>(new RequestSender().sendRequest(groupURL));
                Groups = new JSONParser().ToListWithoutId(groups);
                groupPicker.SelectedItem = null;
                groupPicker.ItemsSource = Groups;
                groupPicker.IsEnabled = true;
            }
        }



        private void ShowSchedule()
        {
            thisSchedule = new GroupScheduler().GetSchedule(finalURL);
            Application.Current.Properties["GroupURL"] = finalURL;
        }

        

        private async void getSchedule_Clicked(object sender, EventArgs e)
        {
            i.IsVisible = true;
            //var task = new Task(ShowSchedule);
            Thread scheduleGettingThread = new Thread(new ThreadStart(ShowSchedule));
            scheduleGettingThread.Start();
            scheduleGettingThread.Join();
            await Navigation.PushAsync(new ScheduleViewingPage(thisSchedule));
            i.IsVisible = false;
        }
    }
}
