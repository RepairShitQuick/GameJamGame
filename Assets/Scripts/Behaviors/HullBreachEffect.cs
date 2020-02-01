using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Behaviors
{
    public class HullBreachEffect : MonoBehaviour
    {
        private int _startingFrameCount;
        private const int _framesToLive = 600;

        private LineRenderer _lineRenderer;

        void Start()
        {
            _startingFrameCount = Time.frameCount;
        }

        void Update()
        {
            if (Time.frameCount > _startingFrameCount + _framesToLive)
            {
                Destroy(_lineRenderer);
                Destroy(this);
            }
            else
            {
                if (_lineRenderer)
                {
                    _lineRenderer.startColor = Color.magenta;
                    _lineRenderer.endColor = Color.magenta;
                }
            }
        }

        public void AddHits(IEnumerable<GameObject> impacts)
        {
            var hits = impacts.Where(t => t != null).Take(2);
            var lineRenderer = new LineRenderer();
            lineRenderer.SetPositions(hits.Select(x => x.transform.position).ToArray());
            _lineRenderer = lineRenderer;
        }
    }
}
