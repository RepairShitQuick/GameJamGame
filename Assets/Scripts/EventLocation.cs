using Assets.Containers;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// This just implies that this class can be targeted to get shot
    /// </summary>
    public class EventLocation : MonoBehaviour
    {
        void Awake()
        {
            EventLocationContainer.AllEventLocations.Add(this);
        }
    }
}
