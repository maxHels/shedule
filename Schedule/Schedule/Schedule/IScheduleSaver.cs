using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Schedule
{
    public interface IScheduleSaver
    {
        bool ExistAsync(string fileName);
        void SaveScheduleAsync(string fileName, GroupSchedule schedule);
        GroupSchedule LoadScheduleAsync(string fileName);
        Task<IEnumerable<string>> GetFilesASync();
    }
}
