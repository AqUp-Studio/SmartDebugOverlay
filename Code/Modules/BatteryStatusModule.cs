using TMPro;
using UnityEngine;

using SmartDebugOverlay.Core;

namespace SmartDebugOverlay.Modules
{
    public class BatteryStatusModule : OverlayModuleBase
    {
        protected override void OnTick()
        {
            float batteryLevel = SystemInfo.batteryLevel * 100;
            BatteryStatus batteryStatus = SystemInfo.batteryStatus;
            base.UpdateTextData($"Battery: {batteryLevel:F0}% ({batteryStatus})");
        }
    }
}