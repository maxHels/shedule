using System;
using Xamarin.Forms;

namespace Schedule
{
    class ScheduleCell : ViewCell
    {
        Label title, descr, startTime, finishTime, teacher, address, room, subgroup;
        string date;
        public ScheduleCell(string date)
        {
            this.date = date;

            title = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold,
                FontSize=15,
            };
            startTime = new Label()
            {
                HorizontalTextAlignment = TextAlignment.End,
                FontAttributes = FontAttributes.Bold,
            };
            finishTime = new Label()
            {
                HorizontalTextAlignment = TextAlignment.End,
                FontAttributes = FontAttributes.Bold,
            };
            teacher = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Center
            };
            address = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Start
            };
            room = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Start
            };
            subgroup = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalOptions=LayoutOptions.Center,   
            };
            descr = new Label()
            {
                HorizontalTextAlignment = TextAlignment.Center,
                LineBreakMode = LineBreakMode.WordWrap,
                HorizontalOptions = LayoutOptions.Center,
            };
            StackLayout cell = new StackLayout() { Orientation = StackOrientation.Vertical };
            StackLayout temlateCell = new StackLayout() { Orientation = StackOrientation.Horizontal };

            StackLayout timeLayout = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.StartAndExpand,
            };
            StackLayout titleLayout = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.Center,
            };
            StackLayout descriptionlayout = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.EndAndExpand,
            };

            Grid timeGrid = new Grid();
            timeGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            timeGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            timeGrid.HorizontalOptions = LayoutOptions.Start;

            timeGrid.Children.Add(startTime, 0, 0);
            timeGrid.Children.Add(finishTime, 0, 1);

            timeLayout.Children.Add(timeGrid);


            Grid titleGrid = new Grid() {WidthRequest=230, HorizontalOptions=LayoutOptions.Center, };
            titleGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            titleGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            titleGrid.Children.Add(subgroup, 0, 0);
            titleGrid.Children.Add(descr, 0, 1);

            title.HorizontalOptions = LayoutOptions.CenterAndExpand;

            titleLayout.Children.Add(titleGrid);

            Grid descriptionGrid = new Grid();
            descriptionGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            descriptionGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            descriptionGrid.Children.Add(address, 0, 0);
            descriptionGrid.Children.Add(room, 0, 1);

            descriptionGrid.HorizontalOptions = LayoutOptions.EndAndExpand;

            descriptionlayout.Children.Add(descriptionGrid);

            cell.Children.Add(title);
            temlateCell.Children.Add(timeLayout);
            temlateCell.Children.Add(titleLayout);
            temlateCell.Children.Add(descriptionlayout);
            cell.Children.Add(temlateCell);
            cell.Children.Add(teacher);

            //var progrBar = new ProgressBar()
            //{
            //    Progress=0.5,

            //};

            //cell.Children.Add(progrBar);

            View = cell;

        }


        public static readonly BindableProperty TitleProperty = BindableProperty.Create("title", typeof(string), typeof(ScheduleCell), "");
        public static readonly BindableProperty DescrPreperty = BindableProperty.Create("Descr", typeof(string), typeof(ScheduleCell), "");
        public static readonly BindableProperty StartTimeProperty = BindableProperty.Create("startTime", typeof(string), typeof(ScheduleCell), "");
        public static readonly BindableProperty FinishTimeProperty = BindableProperty.Create("finishTime", typeof(string), typeof(ScheduleCell), "");
        public static readonly BindableProperty TeacherProperty = BindableProperty.Create("teacher", typeof(string), typeof(ScheduleCell), "");
        public static readonly BindableProperty AddressProperty = BindableProperty.Create("address", typeof(string), typeof(ScheduleCell), "");
        public static readonly BindableProperty RoomProperty = BindableProperty.Create("room", typeof(string), typeof(ScheduleCell), "");
        public static readonly BindableProperty SubgroupProperty = BindableProperty.Create("subgroup", typeof(string), typeof(ScheduleCell), "Нет подгруппы");

        public string Subgroup
        {
            get { return (string)GetValue(SubgroupProperty); }
            set { SetValue(SubgroupProperty, value); }
        }
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public string Descr
        {
            get { return (string)GetValue(DescrPreperty); }
            set { SetValue(DescrPreperty, value); }
        }
        public string StartTime
        {
            get { return (string)GetValue(StartTimeProperty); }
            set { SetValue(StartTimeProperty, value); }
        }
        public string FinishTime
        {
            get { return (string)GetValue(FinishTimeProperty); }
            set { SetValue(FinishTimeProperty, value); }
        }
        public string Teacher
        {
            get { return (string)GetValue(TeacherProperty); }
            set { SetValue(TeacherProperty, value); }
        }
        public string Address
        {
            get { return (string)GetValue(AddressProperty); }
            set { SetValue(AddressProperty, value); }
        }
        public string Room
        {
            get { return (string)GetValue(RoomProperty); }
            set { SetValue(RoomProperty, value); }
        }

        protected override void OnBindingContextChanged()
        {
            if (BindingContext != null)
            {
                title.Text = Title;
                descr.Text = Descr;
                startTime.Text = StartTime;
                finishTime.Text = FinishTime;
                teacher.Text = Teacher;
                address.Text = Address;
                room.Text = Room;
                subgroup.Text = Subgroup;
            }
        }
    }
}



//cell.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
//cell.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(6, GridUnitType.Star) });
//cell.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
//cell.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
//cell.RowDefinitions.Add(new RowDefinition { Height = new GridLength(8,GridUnitType.Star)});
//cell.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Star) });
//cell.RowDefinitions.Add(new RowDefinition { Height = new GridLength(5, GridUnitType.Star) });

/*StackLayout cell = new StackLayout();
StackLayout firstLay = new StackLayout()
{
    Orientation = StackOrientation.Horizontal,
};
StackLayout secLay = new StackLayout()
{
    Orientation = StackOrientation.Horizontal,
};
StackLayout thirdLay = new StackLayout()
{
    Orientation = StackOrientation.Horizontal,
};

firstLay.Children.Add(startTime);
            firstLay.Children.Add(title);
            firstLay.Children.Add(descr);

            secLay.Children.Add(teacher);

            thirdLay.Children.Add(finishTime);
            thirdLay.Children.Add(address);
            thirdLay.Children.Add(room);

            cell.Children.Add(firstLay);
            cell.Children.Add(secLay);
            cell.Children.Add(thirdLay);

            View = cell;*/
//Grid cell = new Grid();
//cell.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1,GridUnitType.Star) });
//cell.RowDefinitions.Add(new RowDefinition { Height = new GridLength(5, GridUnitType.Star) });
//cell.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

/*cell.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
cell.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
cell.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });*/


//DateTime thisDate = DateTime.Now;

//string d = thisDate.ToShortTimeString();
//string k = thisDate.ToLocalTime().TimeOfDay.TotalHours.ToString();
//double thisTime = Convert.ToDouble(thisDate.ToLocalTime().ToString());
//double time=Convert.ToDouble(thisDate.ToLocalTime().TimeOfDay.TotalHours.ToString())
//    +(Convert.ToDouble(thisDate.ToLocalTime().TimeOfDay.TotalMinutes.ToString())/100);
//double startTimed = Convert.ToDouble(StartTime);
//double finishTimed = Convert.ToDouble(FinishTime);
//double decrement = finishTimed - startTimed;
//double percent = decrement / 100;
//int hoursInMillis =(int)(Math.Truncate(finishTimed) - Math.Truncate(thisTime)* 3600000);
//int minutesInMillis = (int)((finishTimed - Math.Truncate(finishTimed)) - (thisTime - Math.Truncate(thisTime))) * 60000;
//int millis = hoursInMillis + minutesInMillis;

//var timeProgress = new ProgressBar();
////if(/*date==thisDate.Date.ToString()*/)
//timeProgress.Progress = ((finishTimed-thisTime)/decrement)*100;

//cell.Children.Add(timeProgress);