using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SmartDebugOverlay.Core;

namespace SmartDebugOverlay.Modules
{
    public class GCButtonModule : OverlayModuleBase
    {
        [SerializeField] private Button gcButton;

        private void Start()
        {
            if (gcButton != null)
                gcButton.onClick.AddListener(Collect);

            base.UpdateTextData("Collect Garbage");
        }

        private void Collect()
        {
            long memoryBefore = GC.GetTotalMemory(false);
            float startTime = Time.realtimeSinceStartup;

            GC.Collect();
            GC.WaitForPendingFinalizers();

            long memoryAfter = GC.GetTotalMemory(true);
            float duration = (Time.realtimeSinceStartup - startTime) * 1000f;

            long memoryFreed = memoryBefore - memoryAfter;
            memoryFreed = (long)Mathf.Max(memoryFreed, 0);

            base.UpdateTextData(
                $"GC Collected!\n" +
                $"Freed: {FormatBytes(memoryFreed)}\n" +
                $"Duration: {duration:F1} ms"
            );

            StartCoroutine(HideMessageAfter(5f));
        }

        private IEnumerator HideMessageAfter(float seconds = 1f)
        {
            yield return new WaitForSeconds(seconds);
            base.UpdateTextData("Collect Garbage");
        }

        private string FormatBytes(long bytes)
        {
            if (bytes >= 1_048_576) // 1 MB
                return $"{bytes / 1_048_576f:F2} MB";
            if (bytes >= 1024)
                return $"{bytes / 1024f:F2} KB";
            return $"{bytes} B";
        }

        protected override void OnTick() { }
    }
}
