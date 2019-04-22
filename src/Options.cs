using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

using TypedTree.Generator.Core.Mapping;
using TypedTree.Generator.Core.Utilities;
using TypedTree.Generator.Editor.Ui;

namespace TypedTree.Generator.Editor
{
    /// <summary>
    /// Configuration options for the scheme mapping.
    /// </summary>
    [Serializable]
    public sealed class Options
    {
#pragma warning disable CS0649
        [Header("Mapping settings")]
        [Tooltip("Fullname of the type to use as the root of the tree")]
        [TypeNamePicker]
        [SerializeField] private string rootAliasType;

        [Tooltip("Enum to indicator how to find fields on types")]
        [SerializeField] private FieldSource fieldSource;

        [Tooltip("Optional regex pattern to ignore types")]
        [SerializeField] private string typeIgnorePattern;

        [Tooltip("Comments to add to nodes")]
        [SerializeField] private NodeComment[] comments;

        [Header("Output")]
        [Tooltip("Auto-generate the scheme on recompile")]
        [SerializeField] private bool autoGenerate = true;

        [Tooltip("Path to save the output file relative to the Assets directory")]
        [SerializeField] private string outputPath = "new.treescheme.json";

        [Header("Diagnostics")]
        [Tooltip("Should verbose diagnostic logging be enabled during the mapping")]
        [SerializeField] private bool verboseLogging;
#pragma warning restore CS0649

        /// <summary>
        /// Fullname of the type to use as the root of the tree.
        /// </summary>
        public string RootAliasTypeName => this.rootAliasType;

        /// <summary>
        /// Enum to indicator how to find fields on types.
        /// </summary>
        public FieldSource FieldSource => this.fieldSource;

        /// <summary>
        /// Optional regex pattern to ignore types.
        /// </summary>
        public Regex TypeIgnorePattern
        {
            get
            {
                if (!string.IsNullOrEmpty(this.typeIgnorePattern))
                {
                    try
                    {
                        return new Regex(this.typeIgnorePattern);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"[TypedTreeGenerator] Invalid regex '{this.typeIgnorePattern}': {e.Message.ToDistinctLines()}");
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Comments to add to nodes.
        /// </summary>
        public IEnumerable<NodeComment> Comments => this.comments;

        /// <summary>
        /// Should the scheme be auto generated on recompile.
        /// </summary>
        public bool AutoGenerate => this.autoGenerate;

        /// <summary>
        /// Path to save the output file relative to the Assets directory.
        /// </summary>
        public string OutputPath => this.outputPath;

        /// <summary>
        /// Should verbose diagnostic logging be enabled during the mapping.
        /// </summary>
        public bool VerboseLogging => this.verboseLogging;
    }
}
