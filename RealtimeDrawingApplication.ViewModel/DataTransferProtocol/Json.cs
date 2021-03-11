using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealtimeDrawingApplication.ViewModel.DataTransferProtocol
{
    public class Json<T>
    {
        public static string SerialisedObject(object model)
        {
            string serialisedString = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            return serialisedString;
        }

        public static List<T> DeserialisedObject(string serialisedString)
        {
            List<T> models = new List<T>();
            models = Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(serialisedString);
            return models;
        }
    }
}
