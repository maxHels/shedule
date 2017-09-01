using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using Xamarin.Forms;
using System.Net.Http;
using System.Threading.Tasks;

namespace Schedule
{
    public class RequestSender
    {
        public string sendRequest(string URL)
        {
            try
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
            catch
            {
                return null;
            }
        }
    }
}