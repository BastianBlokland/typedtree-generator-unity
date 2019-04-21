using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Example.Utils;
using UnityEngine;

namespace Example
{
    public sealed class Spawner : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private float spawnInterval = 2f;

        private IEnumerator Start()
        {
            var spawnPoints = GetSpawnPoints();
            while (true)
            {
                yield return new WaitForSeconds(this.spawnInterval);
                var spawnPoint = spawnPoints.RandomElement();
                GameObject.Instantiate(this.prefab, spawnPoint.position, spawnPoint.rotation);
            }
        }

        private void OnDrawGizmos()
        {
            foreach (var spawnPoint in this.GetSpawnPoints())
            {
                Gizmos.matrix = spawnPoint.localToWorldMatrix;
                Gizmos.DrawSphere(Vector3.zero, 1f);
                Gizmos.DrawCube(new Vector3(0f, 0f, 1f), new Vector3(.2f, .2f, 2f));
            }
        }

        private IEnumerable<Transform> GetSpawnPoints() =>
            this.transform.GetComponentsInChildren<Transform>().Where(t => t != this.transform);
    }
}
