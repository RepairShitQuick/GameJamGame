using UnityEngine;

namespace Assets.Networking.Messaging.Requests
{
    public class CreateRequest
    {
        public string ResourcePath { get; set; }

        public GameObject CreateObject()
        {
            return (GameObject) Resources.Load(ResourcePath);
        }
    }
}
