using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule
{
    public interface IService
    {
        void Start(string URL);
        GroupSchedule GetGroupSchedule(string URL);
        void OnCreate();
    }
}
