#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Agricosmic.Utilities
{
    //https://discussions.unity.com/t/how-to-make-a-readonly-property-in-inspector/75448 <3
    
    public class ReadOnlyAttribute : PropertyAttribute
    {
    }

    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position,
            SerializedProperty property,
            GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
}
#endif