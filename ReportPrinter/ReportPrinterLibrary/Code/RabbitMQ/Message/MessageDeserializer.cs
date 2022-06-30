using System;
using System.IO;
using System.Xml.Serialization;
using ReportPrinterLibrary.Code.Log;

namespace ReportPrinterLibrary.Code.RabbitMQ.Message
{
    public class MessageDeserializer<T>
    {
        public static bool Deserialize(string xml, out T message)
        {
            message = default(T);
            var procName = $"MessageDeserializer.{nameof(Deserialize)}";

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
    }
}