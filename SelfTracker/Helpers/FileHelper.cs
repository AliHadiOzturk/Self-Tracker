using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;

namespace SelfTracker.Helpers
{
    public class FileHelper
    {
        public static void GetFile()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SelfTracker.db");
            var data = File.ReadAllBytes(path);


        }
    }
}
