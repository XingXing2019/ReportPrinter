using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
            
            if (IsSerializable(typeof(T)))
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
            
            if (IsSerializable(typeof(T)))
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
        }

        public static string CreateRedisKey(string managerName, Guid messageId, string itemId)
        {
            return $"{managerName}_{messageId}_{itemId}";
        }


        #region Helper

        private static bool IsSerializable(Type type)
        {
            var serializable = type.GetCustomAttributes(typeof(SerializableAttribute), true).FirstOrDefault();
            var isSerializable = serializable != null;

            return isSerializable;
        }

        #endregion
    }
}