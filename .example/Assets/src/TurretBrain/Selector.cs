using System.Collections.Generic;
using UnityEngine;

namespace Example.TurretBrain
{
    public sealed class Selector : INode
    {
        private readonly IReadOnlyList<INode> children;

        public Selector(IReadOnlyList<INode> children) =>
            this.children = children;

        public NodeResult Evaluate(Turret turret, IEnumerable<Transform> targets)
        {
            foreach (var child in this.children)
            {
                var childResult = child.Evaluate(turret, targets);
                switch (childResult)
                {
                    case NodeResult.Running:
                        return NodeResult.Running;
                    case NodeResult.Success:
                        return NodeResult.Success;
                }
            }

            return NodeResult.Failure;
        }
    }
}
