using System.Collections;
using UnityEngine;

namespace Example
{
    public sealed class Turret : MonoBehaviour
    {
        [SerializeField] private TextAsset brainJson;
        [SerializeField] private float rotationSpeed = 25f;
        [SerializeField] private float timeBetweenShots = .1f;
        [SerializeField] private float fireCooldown = 4f;
        [SerializeField] private GameObject projectile;

        private float lastFireTimestamp;
        private TurretBrain.Brain brain;

        public Vector3 Position => this.transform.position;
        public Transform Target { get; set; }
        public Quaternion InitialRotation { get; private set; }
        public bool Fireing { get; private set; }
        public float IdleTime => Time.time - this.lastFireTimestamp;

        public void Aim(Vector3 targetDirection)
        {
            var targetRotation = Quaternion.LookRotation(targetDirection);
            this.transform.rotation = Quaternion.RotateTowards(
                from: this.transform.rotation,
                to: targetRotation,
                maxDegreesDelta: this.rotationSpeed * Time.deltaTime);
        }

        public Coroutine Fire(int shots = 1)
        {
            if (this.Fireing)
            {
                Debug.LogError($"[{nameof(Turret)}] Unable to fire: Already firing", this);
                return null;
            }

            return StartCoroutine(FireRoutine());

            IEnumerator FireRoutine()
            {
                this.Fireing = true;
                for (int i = 0; i < shots; i++)
                {
                    if (i != 0)
                        yield return new WaitForSeconds(this.timeBetweenShots);

                    GameObject.Instantiate(this.projectile, this.transform.position, this.transform.rotation);
                    this.lastFireTimestamp = Time.time;
                }

                yield return new WaitForSeconds(this.fireCooldown);
                this.Fireing = false;
            }
        }

        private void Start()
        {
            InitialRotation = this.transform.rotation;
            this.brain = TurretBrain.Brain.Parse(this.brainJson.text);
        }

        private void Update() =>
            this.brain?.Execute(this, TargetMarker.Targets);
    }
}
