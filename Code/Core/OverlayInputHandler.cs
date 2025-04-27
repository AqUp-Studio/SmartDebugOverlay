using System;
using UnityEngine;

namespace SmartDebugOverlay.Core
{
    [RequireComponent(typeof(OverlayUISetup))]
    [DisallowMultipleComponent]
    public class OverlayInputHandler : MonoBehaviour
    {
        public event Action OverlayToggled;

        #region Unity Methods

        private void Update() =>
            HandleInput();

        #endregion

        #region Input Handling Methods

        private void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.F1))
                ToggleVisibility();
        }

        private void ToggleVisibility()
        {
            SmartDebugOverlay.SetVisiblity(!SmartDebugOverlay.IsVisible);
            OverlayToggled?.Invoke();
        }

        #endregion
    }
}
