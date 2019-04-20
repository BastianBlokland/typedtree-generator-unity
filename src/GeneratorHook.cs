#if UNITY_EDITOR
using System.Linq;
using UnityEngine;

namespace TypedTree.Generator.Editor
{
    /// <summary>
    /// Class for hooking into Unity events / menus.
    /// </summary>
    public static class GeneratorHook
    {
        [UnityEditor.Callbacks.DidReloadScripts]
        private static void OnScriptsReloaded() => RegenerateAll(onlyAutomaticSchemes: true);

        [UnityEditor.MenuItem("Assets/TypedTree/Regenerate")]
        private static void RegenerateAll() => RegenerateAll(onlyAutomaticSchemes: false);

        private static void RegenerateAll(bool onlyAutomaticSchemes)
        {
            foreach (var generator in UnityEditor.AssetDatabase.
                FindAssets($"t:{nameof(Generator)}").
                Select(g => UnityEditor.AssetDatabase.GUIDToAssetPath(g)).
                Select(p => UnityEditor.AssetDatabase.LoadAssetAtPath<Generator>(p)))
            {
                if (generator != null)
                    generator.Generate(onlyAutomaticSchemes);
            }
        }
    }
}
#endif
