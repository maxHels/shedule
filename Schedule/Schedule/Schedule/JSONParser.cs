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
            if (JSON != null)
            {
                T temp;
                temp = JsonConvert.DeserializeObject<T>(JSON);
                return temp;
            }
            else
            {
                return default(T);
            }
        }
        public List<string> ToListWithoutId(JSONArray arr)
        {
            List<string> list = new List<string>();
            if (arr != null)
            {
                foreach (JSONItem i in arr.items)
                {
                    list.Add(i.title);
                }
                return list;
            }
            return null;
        }
    }
}
