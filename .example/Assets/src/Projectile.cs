using UnityEngine;

namespace Example
{
    public sealed class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed = 25f;
        [SerializeField] private float damage = 10f;
        [SerializeField] private float maxDistance = 100f;

        float distancedTravelled = 0f;

        private void Update()
        {
            var currentPosition = this.transform.position;
            var direction = this.transform.forward;
            var distance = this.speed * Time.deltaTime;

            // Ray for any object in our path of travel.
            if (Physics.Raycast(currentPosition, direction, out var hitInfo, distance))
            {
                // Damage target.
                var targetHealth = hitInfo.collider.gameObject.GetComponent<Health>();
                if (targetHealth != null)
                    targetHealth.Damage(this.damage);

                // Adjust distance we actually travelled before hitting anything.
                distance = hitInfo.distance;

                // Destroy projectile.
                GameObject.Destroy(this.gameObject);
            }

            // Destroy if travelled too far.
            this.distancedTravelled += distance;
            if (this.distancedTravelled > this.maxDistance)
                GameObject.Destroy(this.gameObject);

            // Update position.
            this.transform.position = this.transform.position + direction * distance;
        }
    }
}
