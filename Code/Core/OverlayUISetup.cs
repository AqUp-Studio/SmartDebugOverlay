using UnityEngine;
using SmartDebugOverlay.Core.Utils;

namespace SmartDebugOverlay.Core
{
    [RequireComponent(typeof(CanvasGroup))]
    [RequireComponent(typeof(OverlayInputHandler))]
    [DisallowMultipleComponent]
    public class OverlayUISetup : MonoBehaviour
    {
        private CanvasGroup canvasGroup;
        private OverlayInputHandler inputHandler;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            inputHandler = GetComponent<OverlayInputHandler>();

            if (SmartUtils.FindFirstObjectSafe<OverlayManager>() == null)
                Debug.LogError("OverlayManager doesn't exist in scene! Please create it from SmartDebugOverlay > OverlayManager");
            

            if (inputHandler == null)
            {
                Debug.LogError("OverlayInputHandler is null. Please check UISetup.");
                return;
            }
            OnOverlayToggle(); // initial call : fix
            inputHandler.OverlayToggled += OnOverlayToggle;
        }

        private void OnDestroy() =>
            inputHandler.OverlayToggled -= OnOverlayToggle;
        private void OnOverlayToggle()
        {
            bool isVisible = SmartDebugOverlay.IsVisible;
            canvasGroup.alpha = !isVisible ? 0 : 1;
            canvasGroup.interactable = isVisible;
            canvasGroup.blocksRaycasts = isVisible;
        }
    }
}