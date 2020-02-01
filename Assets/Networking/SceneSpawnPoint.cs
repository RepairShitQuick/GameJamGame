using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Networking
{
    /// <summary>
    /// Flag class to find the spawn point in a scene
    /// </summary>
    public class SceneSpawnPoint : MonoBehaviour
    {
        void Awake()
        {
            if (ServerClientInfo.IsServer)
            {
                var gameObject = new GameObject();
                gameObject.AddComponent<Server>();
            }
        }
    }
}
