using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

namespace Assets.Containers
{
    /// <summary>
    /// Container for all the event locals
    /// </summary>
    public static class EventLocationContainer
    {
        public static readonly IReadOnlyList<EventLocation> AllEventLocations;

        static EventLocationContainer()
        {
            AllEventLocations = GameObject.FindObjectsOfType<EventLocation>();
        }
    }
}
