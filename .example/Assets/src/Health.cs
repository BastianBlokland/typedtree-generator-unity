using System.Collections;
using UnityEngine;

namespace Example
{
    public sealed class Health : MonoBehaviour
    {
        [SerializeField] private float health = 100f;

        public void Damage(float amount)
        {
            this.health -= amount;
            if (this.health < 0)
                Destroy(this.gameObject);
        }
    }
}
