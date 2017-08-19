using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule
{
    class GroupScheduler
    {
        public GroupSchedule GetSchedule(string URL)
        {
            GroupSchedule schedule;
            schedule = new JSONParser().JSONToObjectParsing<GroupSchedule>(new RequestSender().sendRequest(URL));
            return schedule;
        }
        public bool HasBeenChanged(GroupSchedule oldSchedule, GroupSchedule newSchedule)
        {
            if (oldSchedule != newSchedule)
                return true;
            else
                return false;
        }

    }
}
