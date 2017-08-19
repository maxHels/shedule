using Xamarin.Forms;

namespace Schedule
{
    class ScheduleCell:ViewCell
    {
        Label title,descr, startTime, finishTime, teacher, address, room;
        public ScheduleCell()
        {
            title = new Label();
            title.HorizontalTextAlignment = TextAlignment.Center;
            descr = new Label();
            descr.HorizontalTextAlignment = TextAlignment.End;
            startTime = new Label();
            startTime.HorizontalTextAlignment = TextAlignment.Start;
            finishTime = new Label();
            finishTime.HorizontalTextAlignment = TextAlignment.Start;
            teacher = new Label();
            teacher.HorizontalTextAlignment = TextAlignment.Center;
            address = new Label();
            address.HorizontalTextAlignment = TextAlignment.Center;
            room = new Label();
            room.HorizontalTextAlignment = TextAlignment.End; 

            Grid cell = new Grid();
            
            cell.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            cell.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            cell.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            cell.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            cell.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            cell.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            cell.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            cell.Children.Add(startTime, 0, 0);
            cell.Children.Add(finishTime, 0, 1);
            cell.Children.Add(title, 1, 0);
            cell.Children.Add(descr, 2, 0);
            cell.Children.Add(teacher, 1, 1);
            cell.Children.Add(address, 1, 2);
            cell.Children.Add(room, 2, 2);


            View = cell;
        }


        public static readonly BindableProperty TitleProperty = BindableProperty.Create("title", typeof(string), typeof(ScheduleCell), "");
        public static readonly BindableProperty DescrPreperty = BindableProperty.Create("Descr", typeof(string), typeof(ScheduleCell), "");
        public static readonly BindableProperty StartTimeProperty = BindableProperty.Create("startTime", typeof(string), typeof(ScheduleCell), "");
        public static readonly BindableProperty FinishTimeProperty = BindableProperty.Create("finishTime", typeof(string), typeof(ScheduleCell), "");
        public static readonly BindableProperty TeacherProperty = BindableProperty.Create("teacher", typeof(string), typeof(ScheduleCell), "");
        public static readonly BindableProperty AddressProperty = BindableProperty.Create("address", typeof(string), typeof(ScheduleCell), "");
        public static readonly BindableProperty RoomProperty = BindableProperty.Create("room", typeof(string), typeof(ScheduleCell), "");


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