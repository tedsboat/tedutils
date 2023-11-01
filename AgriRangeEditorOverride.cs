#if UNITY_EDITOR
using Agricosmic.Utilities;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(AgriRange), true)]
public class AgriRangeEditorOverride : PropertyDrawer 
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        var minLabelRect = new Rect(position.x, position.y, 20, position.height);
        EditorGUI.PrefixLabel(minLabelRect, new GUIContent("min"));
        var minRect = new Rect(position.x + 25, position.y, 40, position.height);
        EditorGUI.PropertyField(minRect, property.FindPropertyRelative("min"), GUIContent.none);
        var maxLabelRect = new Rect(position.x + 70, position.y, 20, position.height);
        EditorGUI.PrefixLabel(maxLabelRect, new GUIContent("max"));
        var maxRect = new Rect(position.x + 100, position.y, 40, position.height);
        EditorGUI.PropertyField(maxRect, property.FindPropertyRelative("max"), GUIContent.none);
        
        EditorGUI.indentLevel = indent;
        
        EditorGUI.EndProperty();
    }
}
#endif