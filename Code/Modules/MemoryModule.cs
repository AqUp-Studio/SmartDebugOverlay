using System;
using UnityEngine;

using TMPro;

using SmartDebugOverlay.Core;
using UnityEngine.Profiling;

namespace SmartDebugOverlay.Modules
{
    public class MemoryModule : OverlayModuleBase
    {
        protected override void OnTick()
        {
            float gpuUsage = Profiler.GetAllocatedMemoryForGraphicsDriver() / (1024f * 1024f);
            float totalUsage = Profiler.GetTotalAllocatedMemoryLong() / (1024f * 1024f);
            float gcUsage = GC.GetTotalMemory(false) / (1024f * 1024f);
            base.UpdateTextData($"GC: {gcUsage:F1} MB || GPU: {gpuUsage:F1} MB || Total: {totalUsage:F1}");
        }
    }
}