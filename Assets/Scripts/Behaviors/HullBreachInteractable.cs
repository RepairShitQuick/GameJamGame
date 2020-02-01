using Assets.Networking.Identity;

namespace Assets.Scripts.Behaviors
{
    public class HullBreachInteractable : BaseNetworkBehavior
    {
        public void Interact()
        {
            Destroy(this.gameObject);
        }
    }
}
