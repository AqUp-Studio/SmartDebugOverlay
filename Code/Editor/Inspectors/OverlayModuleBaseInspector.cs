using UnityEditor;
using UnityEngine;

namespace SmartDebugOverlay.SmartEditor.Inspectors
{
    [CustomEditor(typeof(Core.OverlayModuleBase), true)]
    public class OverlayModuleBaseInspector : Editor
    {
        private GUIStyle descriptionStyle;

        private void OnEnable()
        {
            descriptionStyle = new GUIStyle()
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
            if (descriptionStyle != null)
                EditorGUILayout.LabelField(
                    "Overlay module for Smart Debug Overlay.",
                    descriptionStyle
                );

            GUILayout.Space(10);
            base.DrawDefaultInspector();
        }
    }
}
