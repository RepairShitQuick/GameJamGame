using UnityEngine;

namespace Assets.Networking
{
    /// <summary>
    /// Mono behavior, will be sent back BY the server to spawn the player
    /// </summary>
    public class PlayerSpawner : MonoBehaviour
    {
        public GameObject PlayerPrefab;

        void Start()
        {
            Instantiate(PlayerPrefab, GameObject.FindObjectOfType<SceneSpawnPoint>().transform);
        }
    }
}
