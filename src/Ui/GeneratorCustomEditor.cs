#if UNITY_EDITOR
using UnityEngine;

namespace TypedTree.Generator.Editor.Ui
{
    /// <summary>
    /// Custom editor ui for <see cref="Generator"/>.
    /// </summary>
    [UnityEditor.CustomEditor(typeof(Generator))]
    public class GeneratorSettingsCustomEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var generator = target as Generator;
            if (generator == null)
                return;

            if (GUILayout.Button("Generate"))
                generator.Generate(onlyAutomaticSchemes: false);
        }
    }
}
#endif
