#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Agricosmic.Utilities
{
    [CustomEditor(typeof(Shaker))]
    public class ShakerDebugger : Editor
    {
        private float _time = 0.25f;
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Shaker shaker = target as Shaker;
            if (shaker == null) return;
            
            EditorGUILayout.Separator();
            
            EditorGUILayout.LabelField("Debugger", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
            _time = EditorGUILayout.FloatField("Time", _time);
            if (GUILayout.Button("Shake!")) shaker.Shake(_time);
            EditorGUILayout.EndHorizontal();;
                
        }
    }
}
#endif