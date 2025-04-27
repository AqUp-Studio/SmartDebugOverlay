using TMPro;
using UnityEngine;

using SmartDebugOverlay.Core;

namespace SmartDebugOverlay.Modules
{
    public class FrameTimeModule : OverlayModuleBase
    {
        private float deltaTime;

        protected override void OnTick()
        {
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
            float frameTime = deltaTime * 1000;
            base.UpdateTextData($"Frame Time: {frameTime:F2} ms");
        }
    }
}