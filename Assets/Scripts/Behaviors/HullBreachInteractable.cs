using UnityEngine;

namespace Assets.Scripts.Behaviors
{
    public class HullBreachInteractable : MonoBehaviour
    {
        public void Start()
        {
            FindObjectOfType<OxygenHandler>().HullBreaches++;
            Debug.Log("Adding to hull breaches counter");
        }

        public void OnDestroy()
        {
            FindObjectOfType<OxygenHandler>().HullBreaches--;

            Debug.Log("Decrementing to hull breaches counter");
        }

        public void Interact()
        {
            Destroy(this.gameObject);
        }
    }
}
