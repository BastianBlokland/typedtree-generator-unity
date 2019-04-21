using System.Collections.Generic;
using UnityEngine;

namespace Example.TurretBrain
{
    public sealed class WaitForIdle : INode
    {
        private readonly float minIdleTime;

        public WaitForIdle(float minIdleTime)
        {
            this.minIdleTime = minIdleTime;
        }

        public NodeResult Evaluate(Turret turret, IEnumerable<Transform> targets)
        {
            return turret.IdleTime >= this.minIdleTime ? NodeResult.Success : NodeResult.Running;
        }
    }
}
