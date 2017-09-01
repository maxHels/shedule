using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Schedule
{
    public interface IScheduleSaver
    {
        bool Exist(string fileName);
        void SaveObject<T>(string fileName, T schedule);
        void SaveText(string fileName, string text);
        T LoadSavedObject<T>(string fileName);
        string LoadText(string fileName);
        Task<IEnumerable<string>> GetFilesASync();
    }
}
