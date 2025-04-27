using UnityEditor;
using UnityEngine;
using SmartDebugOverlay.Core;
using SmartDebugOverlay.Core.Utils;

namespace SmartDebugOverlay.SmartEditor
{
    public static class SmartOverlayCreator
    {
        public static GameObject fullPrefabSet;
        [MenuItem("GameObject/Smart Debug Overlay/Overlay Manager", false, 8)]
        public static void CreateOverlayManager(MenuCommand menuCommand)
        {
            if (SmartUtils.FindFirstObjectSafe<OverlayManager>() != null)
            {
                Debug.LogWarning("OverlayManager already exists in the scene.");
                return;
            }

            GameObject go = new GameObject("OverlayManager");
            go.AddComponent<OverlayManager>();

            GameObjectUtility.SetParentAndAlign(go, ContextParentOrNull(menuCommand));

            Undo.RegisterCreatedObjectUndo(go, "Create OverlayManager");
            Selection.activeObject = go;

            Debug.Log("Smart Debug Overlay Manager created.");
        }

        [MenuItem("GameObject/Smart Debug Overlay/Full UI Setup (Spawner)", false, 8)]
        public static void CreateUI(MenuCommand menuCommand)
        {
            if (SmartUtils.FindFirstObjectSafe<OverlayUISetup>() != null)
            {
                Debug.LogWarning("Some Setup already exists in scene. Ignoring");
            }
            
            var go = new GameObject("_SmartOverlaySpawner");
            go.AddComponent<SmartOverlaySpawner>();

            GameObjectUtility.SetParentAndAlign(go, ContextParentOrNull(menuCommand));

            EditorApplication.delayCall += () =>
            {
                if (go != null)
                    (go.GetComponent<SmartOverlaySpawner>()).Setup();
            };
        }

        private static GameObject ContextParentOrNull(MenuCommand menuCommand) =>
            menuCommand != null ? menuCommand.context as GameObject : null;
    }
}