using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Networking.Identity;
using Assets.Networking.Utilities;
using Newtonsoft.Json;
using UnityEngine;

namespace Assets.Networking.Messaging
{
    public abstract class BaseListener : MonoBehaviour
    {
        public abstract IEnumerable<string> GetMessages();

        void Update()
        {
            var updates = GetMessages();
            foreach (var update in updates)
            {
                var wrapper = DeserializeWrapper(update);
                if (!IdentityStore.NetworkedGameObjectsByGuid.ContainsKey(wrapper.NetworkGuid))
                {
                    var newGameObj = new GameObject();
                    newGameObj.AddComponent<NetworkIdentity>().Guid = wrapper.NetworkGuid;
                    newGameObj.AddComponent(TypeNamer.GetType(wrapper.TypeName));
                }

                var type = TypeNamer.GetType(wrapper.TypeName);

                if (type == typeof(DeleteRequest))
                {
                    var deleteRequest = wrapper.ConvertObjToType<DeleteRequest>();
                    var gameObjectToDeleteComponentFrom = IdentityStore.NetworkedGameObjectsByGuid[deleteRequest.GuidToDelete];
                    Destroy(gameObjectToDeleteComponentFrom.GetComponent(deleteRequest.TypeOfComponent));
                    if (gameObjectToDeleteComponentFrom.GetComponents(typeof(Component)).Length == 0)
                    {
                        Destroy(gameObjectToDeleteComponentFrom);
                    }
                }
                else
                {
                    var comp = IdentityStore.NetworkedGameObjectsByGuid[wrapper.NetworkGuid]
                                            .GetComponent(type);
                    JsonUpdateWriter.UpdateComponent(comp, wrapper.Object);
                }
            }
        }

        private static MessageWrapper DeserializeWrapper(string val)
        {
            return JsonConvert.DeserializeObject<MessageWrapper>(val);
        }
    }
}
