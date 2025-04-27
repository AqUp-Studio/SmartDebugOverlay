using UnityEditor;
using UnityEngine;

namespace SmartDebugOverlay.Core.Utils
{
    public static class SmartUtils
    {
        /// <summary>
        /// Generates a random color between <paramref name="min"/> and <paramref name="max"/>.
        /// </summary>
        /// <param name="min">Minimum color value (default is black).</param>
        /// <param name="max">Maximum color value (default is white).</param>
        /// <returns>Randomly generated color.</returns>
        public static Color RandomColor(Color? min = null, Color? max = null)
        {
            Color _min = min ?? Color.black;
            Color _max = max ?? Color.white;

            return new Color(
                Random.Range(_min.r, _max.r),
                Random.Range(_min.g, _max.g),
                Random.Range(_min.b, _max.b),
                Random.Range(_min.a, _max.a)
            );
        }

        public static T FindFirstObjectSafe<T>() where T : Object
        {
#if UNITY_2023_1_OR_NEWER
            return Object.FindFirstObjectByType<T>();
#else
            return Object.FindObjectOfType<T>();
#endif
        }

        public static T[] FindObjectsSafe<T>() where T : Object
        {
#if UNITY_2023_1_OR_NEWER
            return Object.FindObjectsByType<T>(FindObjectsSortMode.InstanceID);
#else
            return GameObject.FindObjectsOfType<T>();
#endif
        }
    }
}