using UnityEditor;
using UnityEngine;

namespace SmartDebugOverlay.SmartEditor
{
    [InitializeOnLoad]
    public static class SmartDebugStartup
    {
        static SmartDebugStartup()
        {
            if (!EditorPrefs.GetBool("SmartDebugOverlay_WelcomeShown", false))
            {
                ShowWelcomeWindow();
            }
        }

        private static void ShowWelcomeWindow()
        {
            if (EditorWindow.HasOpenInstances<SmartDebugWelcomeWindow>() == false)
            {
                SmartDebugWelcomeWindow.ShowWindow();
                EditorPrefs.SetBool("SmartDebugOverlay_WelcomeShown", true);
            }
        }
    }
}

