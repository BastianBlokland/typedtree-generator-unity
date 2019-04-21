using System.Collections.Generic;
using UnityEngine;

namespace Example.TurretBrain
{
    public sealed class AimAtTarget : INode
    {
        private readonly float maxAngle;

        public AimAtTarget(float maxAngle)
        {
            this.maxAngle = maxAngle;
        }

        public NodeResult Evaluate(Turret turret, IEnumerable<Transform> targets)
        {
            if (turret.Target == null)
                return NodeResult.Failure;

            // Aim at target.
            var turretForward = turret.transform.forward;
            var directionToTarget = Vector3.Normalize(turret.Target.position - turret.transform.position);
            turret.Aim(directionToTarget);

            // Check if we are inside the threshold.
            var angleToTarget = Vector3.Angle(turretForward, directionToTarget);
            return angleToTarget <= this.maxAngle ? NodeResult.Success : NodeResult.Running;
        }
    }
}
