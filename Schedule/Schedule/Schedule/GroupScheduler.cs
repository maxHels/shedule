using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Schedule
{
    class GroupScheduler
    {
        public GroupSchedule GetSchedule(string URL)
        {
            GroupSchedule schedule;
            schedule = new JSONParser().JSONToObjectParsing<GroupSchedule>(new RequestSender().SendRequest(URL));
            return schedule;
        }

        public string MakeGroupURL(string preview)
        {
            DateTime date = DateTime.Now.Date;
            preview += date.ToString("dd.MM.yyyy");
            if (DependencyService.Get<IScheduleSaver>().Exist("daysCount.txt"))
            {
                try
                {
                    int n = Convert.ToInt32(DependencyService.Get<IScheduleSaver>().LoadText("daysCount.txt"));
                    date = date.AddDays(n - 1);
                }
                catch
                {
                    date = date.AddDays(8);
                }
            }
            else
                date = date.AddDays(8);
            preview += "&dateEnd=";
            preview += date.ToString("dd.MM.yyyy");
            return preview;
        }
    }
}
