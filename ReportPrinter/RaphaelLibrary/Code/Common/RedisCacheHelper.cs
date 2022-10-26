using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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

            var bf = new BinaryFormatter();
            using var stream = new MemoryStream();
            bf.Serialize(stream, obj);
            return stream.ToArray(); ;
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

            var bf = new BinaryFormatter();
            using var stream = new MemoryStream();
            stream.Write(arr, 0, arr.Length);
            stream.Seek(0, SeekOrigin.Begin);
            return (T)bf.Deserialize(stream);
        }
    }
}