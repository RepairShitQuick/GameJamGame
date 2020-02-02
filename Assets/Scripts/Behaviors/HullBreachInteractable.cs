using UnityEngine;

namespace Assets.Scripts.Behaviors
{
    public class HullBreachInteractable : MonoBehaviour
    {
        public void Interact()
        {
            Destroy(this.gameObject);
        }
    }
}
