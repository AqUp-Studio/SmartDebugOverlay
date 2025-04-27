using UnityEngine;
using System.Collections.Generic;
using System;

namespace SmartDebugOverlay.Core
{
    [DefaultExecutionOrder(-901)]
    [DisallowMultipleComponent]
    public class OverlayManager : MonoBehaviour
    {
        private List<OverlayModuleBase> modules = new List<OverlayModuleBase>();

        public static OverlayManager Instance { get; private set; }
        private float updateTimer = 0f;
        private const float UpdateInterval = 0.1f;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("OverlayManager's dupe exist in scene! Removing this one");
                Destroy(gameObject);
                return;
            }
            Instance = this;

            if (SmartDebugOverlay.Manager == null)
                SmartDebugOverlay.SetUp(this);
        }

        public void Register(OverlayModuleBase moduleBase)
        {
            if (moduleBase != null && !modules.Contains(moduleBase))
                modules.Add(moduleBase);
        }

        public void UnRegister(OverlayModuleBase moduleBase)
        {
            if (moduleBase != null && modules.Contains(moduleBase))
                modules.Remove(moduleBase);
        }

        private void Update()
        {
            if (!SmartDebugOverlay.IsValid()) return;
            updateTimer += Time.deltaTime;

            if (updateTimer >= UpdateInterval)
            {
                foreach (var module in modules)
                {
                    module.Tick(this);
                }
                updateTimer = 0f;
            }
        }
    }

    public static class SmartDebugOverlay
    {
        public static OverlayManager Manager { get; private set; }
        public static bool IsVisible { get; private set; }

        /// <summary>
        /// Setups SmartDebugOverlay (only in OverlayManager)
        /// </summary>
        /// <param name="newManager">New Instance of OverlayManager</param>
        public static void SetUp(OverlayManager newManager)
        {
            if (IsValid())
            {
                Debug.LogWarning("OverlayManager is already set up.");
                return;
            }

            if (newManager != null)
                Manager = newManager;
            else
                Debug.LogError("Cannot set up OverlayManager: newManager is null.");
        }

        /// <summary>
        /// Checks if OverlayManager is valid
        /// </summary>
        /// <returns>True if valid</returns>
        public static bool IsValid()
        {
            if (Manager != null)
                return true;

            return false;
        }

        /// <summary>
        /// Register's overlay module in OverlayManager
        /// </summary>
        /// <param name="overlayModuleBase">module to add</param>
        public static void Register(OverlayModuleBase overlayModuleBase)
        {
            if (IsValid())
                Manager.Register(overlayModuleBase);
        }

        /// <summary>
        /// Un-Register's overlay module from OverlayManager.
        /// That means module will never called and updated from OverlayManager
        /// </summary>
        /// <param name="overlayModuleBase">overlay to remove</param>
        public static void UnRegister(OverlayModuleBase overlayModuleBase)
        {
            if (IsValid())
                Manager.UnRegister(overlayModuleBase);
        }

        /// <summary>
        /// Changes Overlay visibility
        /// </summary>
        /// <param name="isVisible">visibility state</param>
        public static void SetVisiblity(bool isVisible) =>
            IsVisible = isVisible;
    }
}