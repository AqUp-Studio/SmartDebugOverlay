using TMPro;
using UnityEngine;

namespace SmartDebugOverlay.Core
{
    [DefaultExecutionOrder(-900)]
    [DisallowMultipleComponent]
    public abstract class OverlayModuleBase : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        private void Awake() =>
            SmartDebugOverlay.Register(this);

        private void OnDestroy() =>
            SmartDebugOverlay.UnRegister(this);

        /// <summary>
        /// Updates overlay text data
        /// </summary>
        /// <param name="newText">new text data</param>
        protected void UpdateTextData(string newText)
        {
            if (text != null)
                text.text = newText;
        }

        public void Tick(OverlayManager manager)
        {
            if (SmartDebugOverlay.Manager == manager)
                OnTick();
        }
        
        /// <summary>
        /// Called per OverlayManager update (per frame)
        /// </summary>
        protected abstract void OnTick();


        protected void OnDrawGizmos()
        {
            if (SmartDebugOverlay.IsVisible)
                OnOverlayDrawGizmos();
        }

        protected void OnDrawGizmosSelected()
        {
            if (SmartDebugOverlay.IsVisible)
                OnOverlayDrawGizmosSelected();
        }
        /// <summary>
        /// Custom OnDrawGizmos handler for overlay
        /// </summary>
        protected virtual void OnOverlayDrawGizmos() { }
        /// <summary>
        /// Custom OnDrawGizmosSelected handler for overlay
        /// </summary>
        protected virtual void OnOverlayDrawGizmosSelected() { }
    }
}