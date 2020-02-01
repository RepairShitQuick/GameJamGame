using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Containers;
using Assets.Utils;
using UnityEngine;
using Color = UnityEngine.Color;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Behaviors
{
    public class HullBreachDamageEvent : MonoBehaviour, IDamageEvent
    {
        public GameObject ImpactPreFab;
        public GameObject LineDrawerPreFab;

        public void RunEvent()
        {
            var eventLocations = EventLocationContainer.AllEventLocations;
            var selectedLocal = eventLocations.First().transform;
            var impacts = CalculateBothRayCastHits(selectedLocal);
            var impactPrefabs = new List<GameObject>();
            foreach (var impact in impacts)
            {
                impactPrefabs.Add(InstantiateAtNormal(impact));
            }

            if (impactPrefabs.Count > 2)
            {
                var lineDrawer = GameObject.Instantiate(LineDrawerPreFab, this.transform);
                var lineDrawerComponent = lineDrawer.GetComponent<HullBreachEffect>();
                lineDrawerComponent.AddHits(impactPrefabs);
            }
        }

        private GameObject InstantiateAtNormal(RaycastHit hit)
        {
            return GameObject.Instantiate(ImpactPreFab, hit.point, Quaternion.Euler(hit.normal));
        }

        private RaycastHit[] CalculateBothRayCastHits(Transform transform)
        {
            var point = PositionWithinArea(transform);
            var startingCast = CreateRandomRay(point);
            var hits = Physics.RaycastAll(startingCast.origin, startingCast.direction);
            Debug.DrawLine(startingCast.origin, startingCast.direction, Color.cyan, 1000f);
            return hits;
        }

        private Vector3 PositionWithinArea(Transform transform)
        {
            return new Vector3(GetBoundedValue(transform.position.x, transform.localScale.x),
                                GetBoundedValue(transform.position.y, transform.localScale.y),
                                GetBoundedValue(transform.position.z, transform.localScale.z));
        }

        private float GetBoundedValue(float size, float scale)
        {
            return Random.Range(0.5f * (size - scale), 0.5f * (size + scale));
        }

        private Ray CreateRandomRay(Vector3 startPoint)
        {
            var angle = QuaternionHelper.RandomQuaternion();
            var newPos = startPoint + angle * new Vector3(1, 0, 0) * 10;
            return new Ray(newPos, startPoint - newPos);
        }
    }
}
