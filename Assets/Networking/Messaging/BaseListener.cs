using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Networking.Identity;
using Assets.Networking.Messaging.Requests;
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
                if (wrapper.NetworkGuid == Guid.Empty)
                {
                    Debug.LogWarning("Network guid came over as empty");
                    if(wrapper.MessageObject == null) continue;
                    wrapper = new MessageWrapper(wrapper.MessageObject, Guid.NewGuid());
                }
                if (!IdentityStore.NetworkedGameObjectsByGuid.ContainsKey(wrapper.NetworkGuid))
                {
                    var newGameObj = new GameObject();
                    newGameObj.AddComponent<NetworkIdentity>().Guid = wrapper.NetworkGuid;
                    newGameObj.AddComponent(TypeNamer.GetType(wrapper.TypeName));
                }

                var type = TypeNamer.GetType(wrapper.TypeName);

                if (type == typeof(CreateRequest))
                {
                    var newGameObj = wrapper.ConvertObjToType<CreateRequest>().CreateObject();
                    var identitiy = newGameObj.GetComponent<NetworkIdentity>();
                    IdentityStore.NetworkedGameObjectsByGuid.Add(identitiy.Guid, newGameObj);
                }
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
                    JsonUpdateWriter.UpdateComponent(comp, wrapper.MessageObject);
                }
            }
        }

        private static MessageWrapper DeserializeWrapper(string val)
        {
            return JsonConvert.DeserializeObject<MessageWrapper>(val);
        }
    }
}
