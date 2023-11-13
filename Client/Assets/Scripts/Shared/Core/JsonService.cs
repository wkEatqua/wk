using Microsoft.IO;
using Newtonsoft.Json;
using System;
#if SERVER
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endif

namespace Shared.Core
{
    public class JsonService
    {
        static int blockSize = 1024;
        static int largeBufferMultiple = 1024 * 1024;
        static int maxBufferSize = 1024 * largeBufferMultiple;

        static Microsoft.IO.RecyclableMemoryStreamManager _manager = new Microsoft.IO.RecyclableMemoryStreamManager(blockSize,
                                                largeBufferMultiple,
                                                maxBufferSize);

        static JsonService()
        {
            //_manager.GenerateCallStacks = true;
            _manager.AggressiveBufferReturn = true;
            _manager.MaximumFreeLargePoolBytes = maxBufferSize * 4;
            _manager.MaximumFreeSmallPoolBytes = 100 * blockSize;
        }

        public static string Serialize<T>(T source, JsonSerializerSettings setting)
        {
            try
            {
#if SERVER
                /// RecyclableMemoryStream will be returned, it inherits MemoryStream, 
                /// however prevents data allocation into the LOH(Large Object Heap)
                using (var memoryStream = _manager.GetStream())
                using (var streamWriter = new System.IO.StreamWriter(memoryStream))
                using (JsonWriter jsonWriter = new JsonTextWriter(streamWriter))
                {
                    var serializer = JsonSerializer.Create(setting);
                    serializer.Serialize(jsonWriter, source);
                    streamWriter.Flush();
                    return System.Text.Encoding.UTF8.GetString(memoryStream.ToArray(), 0, (int)memoryStream.Length);
                }
#else
                var value = JsonConvert.SerializeObject(source, setting);
                return value;
#endif
            }
            catch (Exception ex)
            {
                Console.WriteLine($"JsonSerializationError:{source}:{ex}");
                throw;
            }
        }

        public static string SerializePlainObject<T>(T source)
        {
            return Serialize(source, PlainObjectSerializeSetting);
        }

        public static readonly JsonSerializerSettings PlainObjectSerializeSetting = new JsonSerializerSettings()
        {
            Formatting = Formatting.None,
            TypeNameHandling = TypeNameHandling.None,
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            //Converters = { new Newtonsoft.Json.Converters.VectorConverter() }
        };


        public static Object Deserialize(Type t, string text, JsonSerializerSettings setting)
        {
            try
            {
                var obj = JsonConvert.DeserializeObject(text, t, setting);
                return obj;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"JsonSerializationError:{text}:{ex}");
                throw;
            }
        }

        public static T DeserializePlainObject<T>(string source)
        {
            return (T)DeserializePlainObject(typeof(T), source);
        }

        public static Object DeserializePlainObject(Type t, string source)
        {
            return Deserialize(t, source, PlainObjectSerializeSetting);
        }
    }
}
