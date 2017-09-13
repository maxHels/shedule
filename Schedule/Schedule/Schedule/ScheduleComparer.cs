using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule
{
    public class ScheduleComparer
    {
        public List<Lesson> AddedLessons(GroupSchedule oldSchedule, GroupSchedule refreshedSchedule)
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
    }
}
