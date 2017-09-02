using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Schedule
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FirstLaunchPage : ContentPage
	{
        JSONArray faculties, departments, groups; //переменные для сщхранения данных, полученных от сервера
        List<string> Faculties, Departments, Groups; //здесь те же самые данные, на хранятся в листе для последующего заполнения UI
        string finalURL, groupURL; //строки, хранящие адреса переменных для получения расписания
        RequestSender requestSender = new RequestSender();
        JSONParser parser = new JSONParser();

        public FirstLaunchPage()
        {
            InitializeComponent();
            choosenTreatise.Text = treatiseSelector.Value.ToString();
            faculties = parser.JSONToObjectParsing<JSONArray>(requestSender.sendRequest("http://api.grsu.by/1.x/app1/getFaculties")); //получение факультетов
            Faculties = parser.ToListWithoutId(faculties); //кладем названия факультетов в лист
            facultyPicker.ItemsSource = Faculties; //присваиваем 1ому списку лист с факультетами
            facultyPicker.SelectedIndexChanged += OnGroupCreating;
            departmentPicker.SelectedIndexChanged += OnGroupCreating;
            departments = parser.JSONToObjectParsing<JSONArray>(requestSender.sendRequest("http://api.grsu.by/1.x/app1/getDepartments"));//получение форм образованя
            Departments = parser.ToListWithoutId(departments);//формы образования в лист
            departmentPicker.ItemsSource = Departments;//присваиваем 2ому списку лист формами образ
            treatiseSelector.ValueChanged += OnGroupCreating;
            groupPicker.SelectedIndexChanged += OnFinalUrlCreation;
        }

        

        private void OnFinalUrlCreation(object sender, EventArgs e) //метод, составляющий URL для получения расписания конкретной группы
        {                                                           
            if (groupPicker.SelectedItem != null)
            {
                finalURL = "http://api.grsu.by/1.x/app1/getGroupSchedule?groupId=";
                finalURL += groups.items[groupPicker.SelectedIndex].id;
                finalURL += "&dateStart=";
                getSchedule.IsEnabled = true;
            }
        }

        private void OnGroupCreating(object sender, EventArgs e)
        {
            choosenTreatise.Text = treatiseSelector.Value.ToString();
            GroupCreating();           
        }

        private void GroupCreating() //составляет URL для получения групп, в зависимости от выбранного курса, факультета и формы образования
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


        private async void GetSchedule_Clicked(object sender, EventArgs e) //нажатие на кнопку получить расписание
        {
            DependencyService.Get<IScheduleSaver>().SaveText("groupURL.txt", finalURL);//сохраняем URl для получения группы
            await Navigation.PushAsync(new ScheduleViewingPage()); //переход на след. страницу
        }
    }
}
