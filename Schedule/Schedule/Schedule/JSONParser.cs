using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Schedule
{
    public class JSONParser
    {
        public T JSONToObjectParsing<T>  (string JSON)
        {
            T temp;
            temp = JsonConvert.DeserializeObject<T>(JSON);
            return temp;
        }
        public List<string> ToListWithoutId(JSONArray arr)
        {
            List<string> list = new List<string>();
            foreach (JSONItem i in arr.items)
            {
                list.Add(i.title);
            }
            return list;
        }
    }
}
