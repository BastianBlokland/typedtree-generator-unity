using System.Collections.Generic;
using UnityEngine;

namespace Example.TurretBrain
{
    public sealed class Sleep : INode
    {
        public NodeResult Evaluate(Turret turret, IEnumerable<Transform> targets)
        {
            turret.Aim(turret.InitialRotation * Vector3.forward);
            return NodeResult.Running;
        }
    }
}
