using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Example.Utils
{
    public static class ConfigUtils
    {
        public static T Deserialize<T>(string json) =>
            JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                SerializationBinder = new SerializationBinder()
            });

        private class SerializationBinder : ISerializationBinder
        {
            public void BindToName(Type serializedType, out string assemblyName, out string typeName)
            {
                assemblyName = null;
                typeName = serializedType.FullName;
            }

            public Type BindToType(string assemblyName, string typeName) => Type.GetType(typeName);
        }
    }
}
