using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule
{
        [Serializable]
        public class GroupSchedule
        {
            public int count { get; set; }
            public Day[] days { get; set; }
        }
        [Serializable]
        public class Day
        {
            public string num { get; set; }
            public int count { get; set; }
            public string date { get; set; }
            public Lesson[] lessons { get; set; }

        private bool Equals(Day d)
        {
            if (Object.ReferenceEquals(d, null))
                return false;
            if (Object.ReferenceEquals(this, d))
                return true;
            if (this.GetType() != d.GetType())
                return false;
            return (date == d.date) && (count == count);
        }
    }
        [Serializable]
        public class Lesson
        {
            public string timeStart { get; set; }
            public string timeEnd { get; set; }
            public Teacher teacher { get; set; }
            public string type { get; set; }
            public string title { get; set; }
            public string address { get; set; }
            public string room { get; set; }
            public Subgroup subgroup { get; set; }

        private bool Equals(Lesson l)
        {
            if (Object.ReferenceEquals(l, null))
                return false;
            if (Object.ReferenceEquals(this, l))
                return true;
            if (this.GetType() != l.GetType())
                return false;
            return (timeStart == l.timeStart) && (timeEnd == l.timeEnd) && (title == l.title) && (address == l.address) && (room == l.room);
        }
    }
        [Serializable]
        public class Teacher
        {
            public string id { get; set; }
            public string fullname { get; set; }
            public string post { get; set; }
        }
        [Serializable]
        public class Subgroup
        {
            public string id { get; set; }
            public string title { get; set; }
        }
    }
