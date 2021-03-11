using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;

namespace RealtimeDrawingApplication.Common
{
    public class FileHandlingServices
    {
        public static void SaveFile(string document)
        {
            SaveFileDialog save = new SaveFileDialog()
            {
                Title = "Save File",
                FileName = " "
            };

            if (save.ShowDialog() == true)
            {
                using (StreamWriter streamWriter = new StreamWriter(File.Create(save.FileName)))
                {
                    streamWriter.Write(document);
                }
            }
        }

        public static string OpenFile()
        {
            string fileStream = null;
            OpenFileDialog openFile = new OpenFileDialog()
            {
                Title = "Open File",
                FileName = " "
            };

            if (openFile.ShowDialog() == true)
            {
                fileStream=File.ReadAllText(openFile.FileName);
            }
            return fileStream;
        }
    }
}
