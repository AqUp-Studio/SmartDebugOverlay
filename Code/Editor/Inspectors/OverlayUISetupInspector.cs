using UnityEditor;
using UnityEngine;

namespace SmartDebugOverlay.SmartEditor.Inspectors
{
    [CustomEditor(typeof(Core.OverlayUISetup))]
    public class OverlayUISetupInspector : Editor
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

            GUILayout.Label("Overlay UI Setup", headerStyle);
            GUILayout.Space(5);

            EditorGUILayout.LabelField(
                "Handles the visibility and interaction of the overlay UI.\n\n" +
                "• Requires CanvasGroup and OverlayInputHandler components.\n" +
                "• Automatically responds to user input (F1) to show or hide the overlay.\n" +
                "• Controls CanvasGroup's alpha, interactable, and raycast settings.\n\n" +
                "⚡️ Make sure OverlayManager exists in the scene for proper functionality.",
                descriptionStyle
            );

            GUILayout.Space(10);
        }
    }
}
