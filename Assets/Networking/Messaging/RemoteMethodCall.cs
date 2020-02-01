using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Networking.Utilities;

namespace Assets.Networking.Messaging
{
    public struct RemoteMethodCall
    {
        public string TypeNameOfObject { get; }
        public string MethodName { get; }
        public Guid AssociatedNetworkGuid { get; }
        public IEnumerable<Tuple<string, object>> TypeNamesAndValues { get; }

        public RemoteMethodCall(Type invokedObject, Guid associatedNetworkGuid, string methodName, object[] parameters)
        {
            AssociatedNetworkGuid = associatedNetworkGuid;
            TypeNameOfObject = TypeNamer.GetTypeName(invokedObject);
            MethodName = methodName;
            TypeNamesAndValues =
                parameters.Select(c => new Tuple<string, object>(TypeNamer.GetTypeName(c.GetType()), c));
        }
    }
}
