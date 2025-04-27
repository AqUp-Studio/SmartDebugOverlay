using System.Collections.Generic;
using UnityEngine;

using SmartDebugOverlay.Core;

namespace SmartDebugOverlay.Modules
{
    [DisallowMultipleComponent]
    public class EventLoggerModule : OverlayModuleBase
    {
        [SerializeField] private int maxMessages = 10;
        [SerializeField] private float messageLifetime = 5f;

        private class LogMessage
        {
            public string Text;
            public float Timestamp;
        }

        private List<LogMessage> messages = new List<LogMessage>();
        public static EventLoggerModule Instance { get; private set; }

        private void Awake()
        {
            if (Instance != this && Instance != null)
            {
                Debug.LogWarning("EventLogger has dupe! Destoying current one!");
                Destroy(this);
                return;
            }
            Instance = this;
        }

        private void OnDestroy()
        {
            if (Instance == this)
                Instance = null;
        }

        public void LogEvent(string message)
        {
            messages.Add(new LogMessage { Text = $"[{Time.time:0.00}] {message}", Timestamp = Time.time });

            if (messages.Count > maxMessages)
                messages.RemoveAt(0);

            UpdateText();
        }

        private void UpdateText()
        {
            base.UpdateTextData(string.Join("\n", messages.ConvertAll(m => m.Text)));
        }

        protected override void OnTick()
        {
            messages.RemoveAll(m => Time.time - m.Timestamp > messageLifetime);
            UpdateText();
        }
    }

    public static class SmartLog
    {
        public static void Log(string message)
        {
            if (EventLoggerModule.Instance != null)
                EventLoggerModule.Instance.LogEvent(message);
        }
    }
}
