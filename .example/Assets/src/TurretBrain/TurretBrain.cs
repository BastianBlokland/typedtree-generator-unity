using System.Collections.Generic;
using UnityEngine;

namespace Example.TurretBrain
{
    public sealed class Brain
    {
        private readonly INode rootNode;

        private Brain(INode rootNode) =>
            this.rootNode = rootNode;

        public void Execute(Turret turret, IEnumerable<Transform> targets) =>
            this.rootNode.Evaluate(turret, targets);

        public static Brain Parse(string json)
        {
            var rootNode = Utils.ConfigUtils.Deserialize<INode>(json);
            return new Brain(rootNode);
        }
    }
}
