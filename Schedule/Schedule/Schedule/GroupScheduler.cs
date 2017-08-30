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

        public List<Lesson> EditedLessons(GroupSchedule oldSchedule, GroupSchedule refreshedSchedule)
        {
            if (oldSchedule != null && refreshedSchedule != null)
            {
                List<Lesson> editedLessons = new List<Lesson>();

                foreach (Day d in refreshedSchedule.days)
                {
                    bool isNewDay = true;
                    foreach (Day n in oldSchedule.days)
                    {
                        if (d.date == n.date && (!d.Equals(n)))
                        {
                            isNewDay = false;
                            foreach (Lesson l in n.lessons)
                            {
                                foreach (Lesson a in d.lessons)
                                {
                                    bool hasNewLessons = false;
                                    if (l.Equals(a))
                                    {
                                        hasNewLessons = true;
                                        break;
                                    }
                                    if (hasNewLessons)
                                        editedLessons.Add(l);
                                }
                            }
                        }
                    }
                    if (isNewDay)
                        foreach (Lesson l in d.lessons)
                            editedLessons.Add(l);
                }
                return editedLessons;
            }
            else
                return null;
        }

        public string MakeGroupURL(string preview)
        {
            DateTime date = DateTime.Now.Date;
            preview += date.ToString("dd.MM.yyyy");
            date = date.AddDays(8);
            preview += "&dateEnd=";
            preview += date.ToString("dd.MM.yyyy");
            return preview;
        }

    }
}
