namespace Assets.Scripts.Behaviors
{
    public class HullBreachInteractable : NetworkInteractable
    {
        protected override void LocalInteract()
        {
            Destroy(this.gameObject);
        }
    }
}
