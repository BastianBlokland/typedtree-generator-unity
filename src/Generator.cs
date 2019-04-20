using UnityEngine;

namespace TypedTree.Generator.Editor
{
    /// <summary>
    /// ScriptableObject for configuring and running the typedtree-generator.
    /// </summary>
    [CreateAssetMenu(fileName = "TypedTreeGenerator", menuName = "TypedTree/Generator")]
    public sealed class Generator : ScriptableObject
    {
#pragma warning disable CS0649
        [SerializeField] private Options[] schemes = new[] { new Options() };
#pragma warning restore CS0649

        /// <summary>
        /// Generate all schemes configured on this generator.
        /// </summary>
        /// <param name="onlyAutomaticSchemes">
        /// Should schemes not marked with 'autoGenerate' be skipped
        /// </param>
        public void Generate(bool onlyAutomaticSchemes)
        {
            foreach (var options in schemes)
            {
                if (options == null)
                    continue;
                if (onlyAutomaticSchemes && !options.AutoGenerate)
                    continue;
                GeneratorApi.GenerateSchemeToFile(options, logContext: this);
            }
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();
#endif
        }
    }
}
