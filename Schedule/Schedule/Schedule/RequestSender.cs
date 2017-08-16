using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace Schedule
{
    public class RequestSender
    {
        public string sendRequest(string URL)
        {
            string json;
            HttpWebRequest scheduleRequest = (HttpWebRequest)WebRequest.Create(URL);
            HttpWebResponse scheduleResponse = (HttpWebResponse)scheduleRequest.GetResponse();
            using (StreamReader sr = new StreamReader(scheduleResponse.GetResponseStream(), Encoding.UTF8))
            {
                json = sr.ReadToEnd();
            }
            return json;
        }
    }
}
