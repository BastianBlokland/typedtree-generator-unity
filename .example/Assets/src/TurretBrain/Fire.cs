using System.Collections.Generic;
using UnityEngine;

namespace Example.TurretBrain
{
    public sealed class Fire : INode
    {
        private readonly int shots;

        public Fire(int shots) => this.shots = shots;

        public NodeResult Evaluate(Turret turret, IEnumerable<Transform> targets)
        {
            if (!turret.Fireing)
                turret.Fire(this.shots);

            return NodeResult.Running;
        }
    }
}
