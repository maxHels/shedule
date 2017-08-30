using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule
{
    public interface IAlarm
    {
        void SetAlarm();
        void SetAlarm(int dayOfWeek);
    }
}
