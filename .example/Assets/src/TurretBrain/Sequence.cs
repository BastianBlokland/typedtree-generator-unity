using System.Collections.Generic;
using UnityEngine;

namespace Example.TurretBrain
{
    public sealed class Sequence : INode
    {
        private readonly IReadOnlyList<INode> children;

        public Sequence(IReadOnlyList<INode> children) =>
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
                    case NodeResult.Failure:
                        return NodeResult.Failure;
                }
            }

            return NodeResult.Success;
        }
    }
}
