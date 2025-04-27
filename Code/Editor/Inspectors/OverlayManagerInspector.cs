using UnityEditor;
using UnityEngine;

namespace SmartDebugOverlay.SmartEditor.Inspectors
{
    [CustomEditor(typeof(Core.OverlayManager))]
    public class OverlayManagerInspector : Editor
    {
        private GUIStyle headerStyle;
        private GUIStyle descriptionStyle;

        private void OnEnable()
        {
            if (headerStyle == null)
                headerStyle = new GUIStyle(EditorStyles.boldLabel)
                {
                    fontSize = 16,
                    alignment = TextAnchor.MiddleCenter,
                    normal = {
                    textColor = EditorGUIUtility.isProSkin ? Color.white : Color.black
                }
                };

            if (descriptionStyle == null)
                descriptionStyle = new GUIStyle(EditorStyles.label)
                {
                    wordWrap = true,
                    fontSize = 12,
                    normal = {
                    textColor = EditorGUIUtility.isProSkin ? Color.white : Color.black
                }
                };
        }

        public override void OnInspectorGUI()
        {
            GUILayout.Space(10);

            GUILayout.Label("Overlay Manager", headerStyle);
            GUILayout.Space(5);

            EditorGUILayout.LabelField(
                "Acts as the central controller for all Smart Debug Overlay modules.\n" +
                "Handles module registration and updates to maintain a unified overlay system.",
                descriptionStyle
            );

            GUILayout.Space(10);
        }
    }
}
