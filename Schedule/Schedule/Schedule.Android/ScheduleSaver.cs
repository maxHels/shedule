using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Xamarin.Forms;
using Schedule.Droid;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[assembly: Dependency(typeof(ScheduleSaver))]
namespace Schedule.Droid
{
    class ScheduleSaver : IScheduleSaver
    {
        public bool ExistAsync(string fileName)
        {
            string filepath = GetFilePath(fileName);
            bool exists = File.Exists(filepath);
            return exists;
        }

        public Task<IEnumerable<string>> GetFilesASync()
        {
            IEnumerable<string> filenames = from filepath in Directory.EnumerateFiles(GetDocsPath())
                                            select Path.GetFileName(filepath);
            return Task<IEnumerable<string>>.FromResult(filenames);
        }

        public GroupSchedule LoadScheduleAsync(string fileName)
        {
            string filepath = GetFilePath(fileName);
            try
            {
                using (Stream input = File.OpenRead(filepath))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    GroupSchedule schedule;
                    schedule =(GroupSchedule)formatter.Deserialize(input);
                    input.Close();
                    return schedule;
                }
            }
            catch
            {
                return null;
            }
        }

        public void SaveScheduleAsync(string fileName, GroupSchedule schedule)
        {
            string filePath = GetFilePath(fileName);
            using (Stream output = File.Open(filePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                BinaryFormatter writer = new BinaryFormatter();
                writer.Serialize(output, schedule);
                output.Close();
            }
        }

        string GetFilePath(string filename)
        {
            return Path.Combine(GetDocsPath(), filename);
        }

        string GetDocsPath()
        {
            return System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);

        }

    }
}