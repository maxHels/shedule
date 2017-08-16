using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule
{
        public class GroupSchedule
        {
            public int count { get; set; }
            public Day[] days { get; set; }
        }

        public class Day
        {
            public string num { get; set; }
            public int count { get; set; }
            public string date { get; set; }
            public Lesson[] lessons { get; set; }
        }

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
        }

        public class Teacher
        {
            public string id { get; set; }
            public string fullname { get; set; }
            public string post { get; set; }
        }

        public class Subgroup
        {
            public string id { get; set; }
            public string title { get; set; }
        }
    }
