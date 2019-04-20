#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace TypedTree.Generator.Editor.Ui
{
    /// <summary>
    /// Custom property drawer for fields with a <see cref="TypeNamePickerAttribute"/> attribute.
    /// </summary>
    [UnityEditor.CustomPropertyDrawer(typeof(TypeNamePickerAttribute))]
    public class TypeNameDrawer : UnityEditor.PropertyDrawer
    {
        private static string[] typeNames;
        private static string[] typePaths;

        static TypeNameDrawer() => RefreshTypes();

        public override void OnGUI(Rect fullRect, UnityEditor.SerializedProperty property, GUIContent label)
        {
            UnityEditor.EditorGUI.BeginProperty(fullRect, label, property);

            // Verify that the property is a string.
            if (property.propertyType != UnityEditor.SerializedPropertyType.String)
            {
                GUI.Label(fullRect, "You cant use the 'TypeNamePicker' attribute on this type, use it on a 'string'");
                return;
            }

            // Calculate rectangles.
            var buttonWidth = 50f;
            var dropdownRect = new Rect(fullRect.x, fullRect.y, fullRect.width - buttonWidth, fullRect.height);
            var buttonRect = new Rect(fullRect.x + fullRect.width - buttonWidth, fullRect.y, buttonWidth, fullRect.height);

            // Draw a dropdown.
            if (typeNames.Length == 0)
                GUI.Label(dropdownRect, "No types found");
            else
            {
                int selected = Array.IndexOf(typeNames, property.stringValue);
                int newSelection = UnityEditor.EditorGUI.Popup(dropdownRect, label.text, selected, typePaths);
                property.stringValue = newSelection <= 0 ? string.Empty : typeNames[newSelection];
            }

            // Draw button to refresh the types.
            if (GUI.Button(buttonRect, "Refresh"))
                RefreshTypes();

            UnityEditor.EditorGUI.EndProperty();
        }

        private static void RefreshTypes()
        {
            typeNames = GetTypeNames().ToArray();
            typePaths = typeNames.Select(n => n.Replace('.', '/')).ToArray();
        }

        private static IEnumerable<string> GetTypeNames()
        {
            try
            {
                return AppDomain.CurrentDomain.GetAssemblies().
                    Where(a =>
                    {
                        return
                            !a.FullName.StartsWith("mscorlib", StringComparison.InvariantCultureIgnoreCase) &&
                            !a.FullName.StartsWith("netstandard", StringComparison.InvariantCultureIgnoreCase) &&
                            !a.FullName.StartsWith("system", StringComparison.InvariantCultureIgnoreCase) &&
                            !a.FullName.StartsWith("microsoft", StringComparison.InvariantCultureIgnoreCase) &&
                            !a.FullName.StartsWith("mono", StringComparison.InvariantCultureIgnoreCase) &&
                            !a.FullName.StartsWith("unity", StringComparison.InvariantCultureIgnoreCase) &&
                            !a.FullName.StartsWith("nunit", StringComparison.InvariantCultureIgnoreCase);
                    }).
                    SelectMany(a =>
                        a.GetTypes().Where(t =>
                        {
                            try
                            {
                                return
                                    t.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false).Count() == 0 &&
                                    !t.FullName.StartsWith("<PrivateImplementationDetails>", StringComparison.InvariantCultureIgnoreCase);
                            }
                            catch { }
                            return false;
                        })
                    ).
                    Where(t => t.IsClass || (t.IsValueType && !t.IsPrimitive) || t.IsInterface).
                    Select(t => t.FullName);
            }
            catch
            {
                return Array.Empty<string>();
            }
        }
    }
}
#endif
