using TMPro;
using UnityEngine;
using SmartDebugOverlay.Core;

namespace SmartDebugOverlay.Modules
{
    public class FPSModule : OverlayModuleBase
    {
        private float deltaTime;

        protected override void OnTick()
        {
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f; 
            float fps = 1.0f / deltaTime;
            base.UpdateTextData($"FPS: {fps:0.}");
        }
    }
}