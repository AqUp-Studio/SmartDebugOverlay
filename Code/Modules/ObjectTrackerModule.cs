#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

using SmartDebugOverlay.Core.Utils;

namespace SmartDebugOverlay.Core
{
    public enum ObjectType
    {
        None,
        GameObjects,
        Rigidbodies,
        Colliders,
        ParticleSystems,
        AudioSources,
        MonoBehaviours,
        CustomType
    }
    public class ObjectTrackerModule : OverlayModuleBase
    {
        [Header("Object Tracker Settings")]
        [SerializeField] private ObjectType objectType = ObjectType.GameObjects;
        [SerializeField] private string customTypeName;
        [SerializeField] private float updateInterval = 60f;

        private Dictionary<ObjectType, List<GameObject>> cachedObjects = new();
        private Dictionary<string, List<GameObject>> cachedCustomTypes = new();
        private Coroutine _currentCoro;
        private Color _gizmoColor;

        private ObjectType _lastObjectType = ObjectType.None;
        private List<GameObject> foundObjects = new List<GameObject>();
        private float timer;
        private bool isScanning;

        private void OnEnable() =>
            _gizmoColor = SmartUtils.RandomColor();

        protected override void OnTick()
        {
            if (objectType != _lastObjectType)
            {
                StartRefresh();
                _lastObjectType = objectType;
            }

            timer += Time.unscaledDeltaTime;

            if (timer >= updateInterval)
            {
                StartRefresh();
                timer = 0f;
            }

            base.UpdateTextData($"{objectType} Count: {foundObjects.Count}");
        }

        private void StartRefresh()
        {
            // additional layer of protection (pls)
            if (_currentCoro != null)
                StopCoroutine(_currentCoro);
            
            _currentCoro = StartCoroutine(AsyncRefresh());
        }

        private IEnumerator AsyncRefresh()
        {
            if (isScanning) yield break;
            isScanning = true;

            if (objectType == ObjectType.CustomType)
            {
                if (!cachedCustomTypes.TryGetValue(customTypeName, out foundObjects))
                {
                    foundObjects = new List<GameObject>();
                    yield return StartCoroutine(FindCustomTypeObjectsAsync(foundObjects));
                    cachedCustomTypes[customTypeName] = new List<GameObject>(foundObjects);
                }
            }
            else
            {
                if (!cachedObjects.TryGetValue(objectType, out foundObjects))
                {
                    foundObjects = new List<GameObject>();
                    yield return StartCoroutine(FindObjectsAsyncByType(objectType, foundObjects));
                    cachedObjects[objectType] = new List<GameObject>(foundObjects);
                }
            }

            isScanning = false;
            _currentCoro = null;
        }


        private IEnumerator FindObjectsAsyncByType(ObjectType type, List<GameObject> result)
        {
            switch (type)
            {
                case ObjectType.GameObjects:

            foreach (var go in SmartUtils.FindObjectsSafe<GameObject>())
                    {
                        result.Add(go);
                        if (result.Count % 50 == 0)
                            yield return null;
                    }
                    break;

                case ObjectType.Rigidbodies:
                    yield return StartCoroutine(AddComponentsAsync<Rigidbody>(result));
                    break;

                case ObjectType.Colliders:
                    yield return StartCoroutine(AddComponentsAsync<Collider>(result));
                    break;

                case ObjectType.ParticleSystems:
                    yield return StartCoroutine(AddComponentsAsync<ParticleSystem>(result));
                    break;

                case ObjectType.AudioSources:
                    yield return StartCoroutine(AddComponentsAsync<AudioSource>(result));
                    break;

                case ObjectType.MonoBehaviours:
                    yield return StartCoroutine(AddComponentsAsync<MonoBehaviour>(result));
                    break;
            }
        }

        private IEnumerator AddComponentsAsync<T>(List<GameObject> result) where T : Component
        {
            var items = SmartUtils.FindObjectsSafe<T>();

            int i = 0;
            foreach (var comp in items)
            {
                if (comp != null)
                    result.Add(comp.gameObject);
                if (++i % 50 == 0)
                    yield return null;
            }
        }

        private IEnumerator FindCustomTypeObjectsAsync(List<GameObject> result)
        {
            if (string.IsNullOrEmpty(customTypeName))
            {
                Debug.LogError("[ObjectTracker] Custom type name is empty.");
                yield break;
            }
            var allComponents = SmartUtils.FindObjectsSafe<Component>();

            int i = 0;
            foreach (var comp in allComponents)
            {
                if (comp.GetType().Name == customTypeName)
                    result.Add(comp.gameObject);

                if (++i % 10 == 0)
                    yield return null;
            }
        }

        protected override void OnOverlayDrawGizmos()
        {
            if (foundObjects == null || foundObjects.Count == 0)
                return;

            Gizmos.color = _gizmoColor;

            foreach (var obj in foundObjects)
            {
                if (obj == null) continue;

                Bounds bounds;

                var collider = obj.GetComponent<Collider>();
                if (collider != null)
                {
                    bounds = collider.bounds;
                }
                else
                {
                    var renderer = obj.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        bounds = renderer.bounds;
                    }
                    else
                    {
                        var meshFilter = obj.GetComponent<MeshFilter>();
                        if (meshFilter != null && meshFilter.sharedMesh != null)
                        {
                            bounds = meshFilter.sharedMesh.bounds;
                            bounds.center += obj.transform.position;
                        }
                        else
                        {
                            bounds = new Bounds(obj.transform.position, Vector3.one * 0.5f);
                        }
                    }
                }

                Gizmos.DrawWireCube(bounds.center, bounds.size);
            }
        }
    }
}
