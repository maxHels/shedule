using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule
{
        public class JSONArray
        {
            
            public JSONItem[] items { get; set; }
        }

        public class JSONItem
        {
            public string id { get; set; }
            public string title { get; set; }
        }
}
