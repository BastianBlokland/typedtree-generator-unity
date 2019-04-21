using System.Collections.Generic;
using UnityEngine;

namespace Example
{
    public sealed class TargetMarker : MonoBehaviour
    {
        private static HashSet<Transform> targets = new HashSet<Transform>();

        public static IEnumerable<Transform> Targets => targets;

        private void OnEnable() => targets.Add(this.transform);

        private void OnDisable() => targets.Remove(this.transform);
    }
}
