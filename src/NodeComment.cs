using System;
using UnityEngine;

using TypedTree.Generator.Editor.Ui;

namespace TypedTree.Generator.Editor
{
    /// <summary>
    /// Comment about a node.
    /// </summary>
    [Serializable]
    public sealed class NodeComment
    {
#pragma warning disable CS0649
        [Tooltip("Fullname of the node-type to add comment to")]
        [TypeNamePicker]
        [SerializeField] private string nodeType;

        [Tooltip("Comment to add to the node")]
        [SerializeField] private string comment = "Add comment here";
#pragma warning restore CS0649

        /// <summary>
        /// Fullname of the node-type to add comment to.
        /// </summary>
        public string NodeType => this.nodeType;

        /// <summary>
        /// Comment to add to the node.
        /// </summary>
        public string Comment => this.comment;
    }
}
