using UnityEngine;
using UnityEditor;

namespace SmartDebugOverlay.SmartEditor
{
    public class SmartDebugWelcomeWindow : EditorWindow
    {
        private GUIStyle headerStyle;
        private Vector2 scrollPos;

        [MenuItem("Window/Smart Debug Overlay/Setup", priority = 1)]
        public static void ShowWindow()
        {
            var window = GetWindow<SmartDebugWelcomeWindow>("Welcome to Smart Debug Overlay");
            window.minSize = new Vector2(620, 500);
        }

        private void OnEnable()
        {
            headerStyle = new GUIStyle()
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 35,
                fontStyle = FontStyle.Bold,
                normal = { textColor = EditorGUIUtility.isProSkin ? Color.white : Color.black }
            };
        }

        private void OnGUI()
        {
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

            DrawHeader();
            GUILayout.Space(15);
            DrawQuickStart();
            GUILayout.Space(25);
            DrawActionButtons();
            GUILayout.Space(25);
            DrawMoreInfoSection();

            EditorGUILayout.EndScrollView();
        }

        private void DrawHeader()
        {
            GUILayout.Space(5);
            GUILayout.Label("Welcome to Smart Debug Overlay!", headerStyle);
        }

        private void DrawQuickStart()
        {
            EditorGUILayout.LabelField("Quick Start", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox(
                "1. Add 'OverlayManager' to the scene.\n" +
                "2. Add UI with 'Full UI Setup'.\n" +
                "3. Customize and expand modules as needed.",
                MessageType.Info
            );
        }

        private void DrawActionButtons()
        {
            EditorGUILayout.LabelField("Setup", EditorStyles.boldLabel);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Create Overlay Manager", GUILayout.Height(40)))
                SmartOverlayCreator.CreateOverlayManager(null);

            if (GUILayout.Button("Full UI Setup", GUILayout.Height(40)))
                SmartOverlayCreator.CreateUI(null);
            GUILayout.EndHorizontal();
        }

        private void DrawMoreInfoSection()
        {
            EditorGUILayout.LabelField("About Smart Debug Overlay", EditorStyles.boldLabel);

            EditorGUILayout.HelpBox(
                "Smart Debug Overlay is a powerful Unity package designed to help you monitor and debug your game during development. " +
                "It allows you to quickly access important information like FPS, memory usage, logs, and object counts in real-time. " +
                "The overlay provides a seamless debugging experience, offering customizable modules that can be expanded to fit your needs.\n\n" +
                "Key Features:\n" +
                "- Real-time FPS, memory, and performance monitoring\n" +
                "- Customizable modules for displaying logs, object counts, and more\n" +
                "- Easy-to-use UI setup and integration\n\n" +
                "To get the most out of the package, explore the full documentation and tutorials on our GitHub and YouTube channels.",
                MessageType.Info
            );

            EditorGUILayout.Space(15);
            if (GUILayout.Button("Watch Setup Tutorial on YouTube"))
            {
                Application.OpenURL("https://youtu.be/GhpWh69mJaE");
            }

            EditorGUILayout.Space(15);
            if (GUILayout.Button("Visit Documentation on GitHub"))
            {
                Application.OpenURL("https://github.com/AqUp-Studio/SmartDebugOverlay");
            }
        }
    }
}
