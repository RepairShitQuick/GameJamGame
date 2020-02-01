using System;
using Assets.Networking.Utilities;
using Newtonsoft.Json.Linq;

namespace Assets.Networking.Messaging
{
    public struct MessageWrapper
    {
        public Guid NetworkGuid { get;  }
        public string TypeName { get; }
        public object MessageObject { get; set; }

        public MessageWrapper(object obj, Guid guid)
        {
            NetworkGuid = guid;
            TypeName = TypeNamer.GetTypeName(obj.GetType());
            MessageObject = obj;
        }

        public T ConvertObjToType<T>()
        {
            var jObject = (JObject) MessageObject;
            return jObject.ToObject<T>();
        }
    }
}
