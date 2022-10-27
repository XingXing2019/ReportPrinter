using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;
using ReportPrinterLibrary.Code.Log;

namespace RaphaelLibrary.Code.Common
{
    public class RedisCacheHelper
    {
        public static byte[] ObjectToByteArray<T>(T obj)
        {
            var procName = $"RedisCacheHelper.{nameof(ObjectToByteArray)}";

            if (obj == null)
            {
                var error = $"Cannot convert null into byte array";
                Logger.Error(error, procName);
                throw new InvalidOperationException(error);
            }

            var type = typeof(T);
            var serializable = type.GetCustomAttributes(typeof(SerializableAttribute), true).FirstOrDefault() as SerializableAttribute;
            var isSerializable = serializable != null;

            if (isSerializable)
            {
                var bf = new BinaryFormatter();
                using var stream = new MemoryStream();
                bf.Serialize(stream, obj);
                return stream.ToArray();
            }
            else
            {
                using var stream = new MemoryStream();
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stream, obj);
                return stream.ToArray();
            }
           

            //using var writer = new StringWriter();
            //var serializer = new XmlSerializer(typeof(T));
            //serializer.Serialize(writer, obj);
            //var str = writer.ToString();
            //return Encoding.ASCII.GetBytes(str);
        }

        public static T ByteArrayToObject<T>(byte[] arr)
        {
            var procName = $"RedisCacheHelper.{nameof(ByteArrayToObject)}";

            if (arr == null)
            {
                var error = $"Cannot convert null into object";
                Logger.Error(error, procName);
                throw new InvalidOperationException(error);
            }

            var type = typeof(T);
            var serializable = type.GetCustomAttributes(typeof(SerializableAttribute), true).FirstOrDefault() as SerializableAttribute;
            var isSerializable = serializable != null;

            if (isSerializable)
            {
                var bf = new BinaryFormatter();
                using var stream = new MemoryStream();
                stream.Write(arr, 0, arr.Length);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)bf.Deserialize(stream);
            }
            else
            {
                using var stream = new MemoryStream(arr);
                var serializer = new XmlSerializer(typeof(T));
                var obj = serializer.Deserialize(stream);
                return (T)obj;
            }
            

            //var str = Encoding.ASCII.GetString(arr);
            //using var reader = new StringReader(str);
            //var serializer = new XmlSerializer(typeof(T));
            //var obj = serializer.Deserialize(reader);
            //return (T)obj;
        }

        public static string CreateRedisKey(string managerName, Guid messageId, string itemId)
        {
            return $"{managerName}_{messageId}_{itemId}";
        }
    }
}