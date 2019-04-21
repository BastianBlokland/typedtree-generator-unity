using UnityEngine;

namespace Example
{
    public sealed class Move : MonoBehaviour
    {
        [SerializeField] private float speed = 1f;

        private void Update() =>
            this.transform.position += this.transform.forward * this.speed * Time.deltaTime;
    }
}
