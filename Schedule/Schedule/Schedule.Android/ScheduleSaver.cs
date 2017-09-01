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
        public bool Exist(string fileName)
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

        public T LoadSavedObject<T>(string fileName)
        {
            string filepath = GetFilePath(fileName);
            try
            {
                using (Stream input = File.OpenRead(filepath))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    T schedule;
                    schedule = (T)formatter.Deserialize(input);
                    input.Close();
                    return schedule;
                }
            }
            catch
            {
                return default(T);
            }
        }

        public void SaveObject<T>(string fileName, T objectToSave)
        {
            string filePath = GetFilePath(fileName);
            using (Stream output = File.Open(filePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                BinaryFormatter writer = new BinaryFormatter();
                writer.Serialize(output, objectToSave);
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

        public async void SaveText(string fileName, string text)
        {
            string filePath = GetFilePath(fileName);
            using (StreamWriter textWriter = File.CreateText(filePath))
            {
                await textWriter.WriteAsync(text);
            }
        }

        public string LoadText(string fileName)
        {
            string filePath = GetFilePath(fileName);
            try
            {
                using (StreamReader sr = File.OpenText(filePath))
                {
                    return sr.ReadToEnd();
                }
            }
            catch
            {
                return null;
            }
        }
    }
}