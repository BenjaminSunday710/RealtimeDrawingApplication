using Microsoft.Win32;
using RealtimeDrawingApplication.Model;
using RealtimeDrawingApplication.ViewModel.DatabaseServices;
using RealtimeDrawingApplication.ViewModel.Proxies;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace RealtimeDrawingApplication.ViewModel.DataTransferProtocol
{
    public class DataTransferServices
    {
        private static Repository<DrawingComponentModel> database = Repository<DrawingComponentModel>.GetRepository;

        public static void SerializedObjectToXml(string projectName)
        {
            if (projectName == null)
            {
                MessageBox.Show("Invalid Operation!! Cannot share empty project", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var model = database.GetDrawingComponents(projectName);

            SaveFileDialog save = new SaveFileDialog()
            {
                Title = "Save File",
                FileName = " ",
                Filter = "XmlDocument (*.xml) | *.xml"
            };

            if (save.ShowDialog() == true)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<DrawingComponentModel>));
                using (StreamWriter streamWriter = new StreamWriter(File.Create(save.FileName)))
                {
                    serializer.Serialize(streamWriter, model);
                }
            }
        }

        public static List<DrawingComponentProxy> DeserializeObjectFromXml()
        {
            List<DrawingComponentProxy> drawingComponentProxies = new List<DrawingComponentProxy>();
            OpenFileDialog openFile = new OpenFileDialog()
            {
                Title = "Open File",
                FileName = " "
            };

            if (openFile.ShowDialog() == true)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<DrawingComponentModel>));
                using (FileStream fileStream = File.OpenRead(openFile.FileName))
                {
                    drawingComponentProxies = DrawingComponentModelService.DeserializeToProxy((List<DrawingComponentModel>)serializer.Deserialize(fileStream));
                }
            }

            return drawingComponentProxies;
        }

        public static void SerialiseObjectToJson(string projectName)
        {
            if (projectName == null)
            {
                MessageBox.Show("Invalid Operation!! Cannot share empty project", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var drawingComponents = database.GetDrawingComponents(projectName);
            SaveFileDialog save = new SaveFileDialog()
            {
                Title = "Save File",
                FileName = " ",
                Filter = "TextDocument (*.txt) | *.txt"
            };

            if (save.ShowDialog() == true)
            {
                using (StreamWriter streamWriter = new StreamWriter(File.Create(save.FileName)))
                {
                    var serialisedString = Newtonsoft.Json.JsonConvert.SerializeObject(drawingComponents);
                    streamWriter.Write(serialisedString);
                }
            }

        }

        public static List<DrawingComponentProxy> DeserialiseObjectFromJson()
        {
            List<DrawingComponentProxy> drawingComponentProxies = new List<DrawingComponentProxy>();
            OpenFileDialog openFile = new OpenFileDialog()
            {
                Title = "Open File",
                FileName = " "
            };

            string fileStream = null;
            if (openFile.ShowDialog() == true)
            {
                fileStream = File.ReadAllText(openFile.FileName);

                if (fileStream == string.Empty)
                {
                    MessageBox.Show("Imported File is empty", "Notification Message", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    return null;
                }

                drawingComponentProxies = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DrawingComponentProxy>>(fileStream);
            }

            return drawingComponentProxies;
        }
    }
}
