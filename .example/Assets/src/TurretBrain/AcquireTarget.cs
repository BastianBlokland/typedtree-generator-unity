using System.Collections.Generic;
using System.Linq;
using Example.Utils;
using UnityEngine;

namespace Example.TurretBrain
{
    public sealed class AcquireTarget : INode
    {
        public enum Mode
        {
            Closest = 1,
            Random = 2
        }

        private readonly float range;
        private readonly Mode mode;
        private readonly bool preferOldTarget;

        public AcquireTarget(float range, Mode mode, bool preferOldTarget)
        {
            this.range = range;
            this.mode = mode;
            this.preferOldTarget = preferOldTarget;
        }

        public NodeResult Evaluate(Turret turret, IEnumerable<Transform> targets)
        {
            // If current target is still in rang then keep targetting that.
            if (this.preferOldTarget && turret.Target != null && IsInRange(turret.Target))
                return NodeResult.Success;

            // Otherwise find a new target.
            turret.Target = GetTarget();

            // Successfull if we found a target.
            return turret.Target != null ? NodeResult.Success : NodeResult.Failure;

            Transform GetTarget()
            {
                switch (this.mode)
                {
                    case Mode.Closest:
                        return targets.Where(IsInRange).OrderBy(GetSqrDist).FirstOrDefault();
                    case Mode.Random:
                        return targets.Where(IsInRange).RandomElement();
                    default:
                        return null;
                }
            }

            bool IsInRange(Transform target) => GetSqrDist(target) <= (this.range * this.range);

            float GetSqrDist(Transform target) => Vector3.SqrMagnitude(target.position - turret.Position);
        }
    }
}
