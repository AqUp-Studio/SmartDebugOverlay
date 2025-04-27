#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace SmartDebugOverlay.Core.Utils
{
    public class SmartOverlaySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        
        public void Setup()
        {
            GameObject instance = Instantiate(prefab);
            instance.name = prefab.name;

            Undo.RegisterCreatedObjectUndo(instance, "Spawn Smart Debug UI");
            Selection.activeObject = instance;

            // Done, remove helper
            DestroyImmediate(this.gameObject);
        }
    }
}
#endif
