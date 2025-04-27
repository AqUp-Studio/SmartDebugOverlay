using UnityEditor;
using UnityEngine;
using SmartDebugOverlay.Modules;

namespace SmartDebugOverlay.SmartEditor.Inspectors
{
    [CustomEditor(typeof(EventLoggerModule))]
    public class EventLoggerInspector: Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.LabelField("Settings", EditorStyles.boldLabel);

            EditorGUILayout.ObjectField(serializedObject.FindProperty("text"), new GUIContent("Text Component (TMPro)"));

            EditorGUILayout.PropertyField(serializedObject.FindProperty("maxMessages"), new GUIContent("Max Messages"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("messageLifetime"), new GUIContent("Message Lifetime (seconds)"));

            EditorGUILayout.Space(10);
            DrawDocumentation();

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawDocumentation()
        {
            EditorGUILayout.LabelField("How to use:", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox(
                "You can log messages from any script:\n\n" +
                "SmartLog.Log(\"Your message\");\n\n" +
                "The Event Logger will automatically appear in the overlay. If Overlay exist in scene", 
                MessageType.Info
            );
        }
    }
}
