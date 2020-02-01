using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Networking.Identity;
using Assets.Networking.Messaging;
using UnityEngine;

namespace Assets.Networking
{
    /// <summary>
    /// Flag class to find the spawn point in a scene
    /// </summary>
    public class SceneSpawnPoint : MonoBehaviour
    {
        public GameObject PlayerPreFab;
        void Awake()
        {
            if (ServerClientInfo.IsServer)
            {
                var gameObject = new GameObject();
                gameObject.AddComponent<Server>();
                gameObject.AddComponent<ServerRequestListener>();
                gameObject.AddComponent<ClientRequestListener>();
                gameObject.AddComponent<ServerClient>();
            }
            else
            {
                var gameObject = new GameObject();
                gameObject.AddComponent<ServerClient>();
            }


            var copy = Instantiate(PlayerPreFab);
            copy.GetComponent<NetworkedPlayer>().PlayerOwned = true;
            copy.gameObject.transform.position = transform.position;
        }
    }
}
