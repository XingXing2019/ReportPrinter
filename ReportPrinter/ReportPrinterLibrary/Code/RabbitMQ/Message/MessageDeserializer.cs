using System;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;
using ReportPrinterLibrary.Code.Log;

namespace ReportPrinterLibrary.Code.RabbitMQ.Message
{
    public class MessageDeserializer<T>
    {
        public static bool DeserializeXmlMessage(string xml, out T message)
        {
            message = default(T);
            var procName = $"MessageDeserializer.{nameof(DeserializeXmlMessage)}";

            try
            {
                var serializer = new XmlSerializer(typeof(T));
                using var reader = new StringReader(xml);
                message = (T)serializer.Deserialize(reader);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during deserializing \n{xml} into {typeof(T)}. Ex: {ex.Message}", procName);
                return false;
            }
        }

        public static bool DeserializeJsonMessage(string json, out T message)
        {
            message = default(T);
            var procName = $"MessageDeserializer.{nameof(DeserializeJsonMessage)}";

            try
            {
                message = JsonConvert.DeserializeObject<T>(json);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error($"Exception happened during deserializing \n{json} into {typeof(T)}. Ex: {ex.Message}", procName);
                return false;
            }
        }
    }
}