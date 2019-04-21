using System.Collections.Generic;
using UnityEngine;

namespace Example.TurretBrain
{
    public interface INode
    {
        NodeResult Evaluate(Turret turret, IEnumerable<Transform> targets);
    }
}
