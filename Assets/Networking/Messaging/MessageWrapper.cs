using System;
using Assets.Networking.Utilities;
using Newtonsoft.Json.Linq;

namespace Assets.Networking.Messaging
{
    public struct MessageWrapper
    {
        public Guid NetworkGuid { get;  }
        public string TypeName { get; }
        public object Object { get; }

        public MessageWrapper(object obj, Guid guid)
        {
            NetworkGuid = guid;
            TypeName = TypeNamer.GetTypeName(obj.GetType());
            Object = obj;
        }

        public T ConvertObjToType<T>()
        {
            var jObject = (JObject) Object;
            return jObject.ToObject<T>();
        }
    }
}
