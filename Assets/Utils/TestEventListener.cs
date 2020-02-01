using Assets.Networking;
using Assets.Scripts.Behaviors;
using UnityEngine;

namespace Assets.Utils
{
    public class TestEventListener : MonoBehaviour
    {
        public GameObject HullBreachDamageEventPrefab;
        private HullBreachDamageEvent hullBreachDamageEvent;

        public void Start()
        {
            hullBreachDamageEvent = HullBreachDamageEventPrefab.GetComponent<HullBreachDamageEvent>();
        }

        void Update()
        {
            if (ServerClientInfo.IsServer && Input.GetKeyDown(KeyCode.O))
            {
                hullBreachDamageEvent.RunEvent();
            }
        }
    }
}
