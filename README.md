# 📖 Smart Debug Overlay

> **Smart Debug Overlay** is a modular and efficient runtime debug toolkit for Unity.  
> Monitor performance, track scene objects, log events — all while keeping your game running smoothly.

---

## 🚀 Features

- ⚡ **Quick Setup** — Just Create OverlayManager and build your own UI setup.
- 🧩 **Modular** — Build your own debug tools using `OverlayModuleBase`.
- 🔎 **Object Tracking** — Monitor GameObjects, Components, or custom types with async scene scanning.
- 📝 **Event Logging** — Use `SmartLog` to log runtime events easily.
- 🎯 **Editor + Runtime Friendly** — Designed for Unity 2021–2025 and beyond.

---

## 🛠️ Quick Start

1. Create the **OverlayManager** from hierarchy.<br>
![image](https://github.com/user-attachments/assets/3a5247b2-6cd2-4ef0-b3b7-65da6b6d3009)
2. (Optional) Use **Full UI Setup** to auto-create a ready-to-go overlay with sample modules. <br>
![image](https://github.com/user-attachments/assets/bd219638-1429-47cf-876e-8e9b843e3760)
3. Press **F1** at runtime to show/hide the overlay.  
![image](https://github.com/user-attachments/assets/c7319292-5727-4d36-9870-6c516bdf4285)
4. Customize modules or add your own based on `OverlayModuleBase`.  

> ✨ *Overlay updates happen asynchronously to minimize performance impact.*

---

## 🧠 Example: Creating a Custom Module
```csharp
using TMPro;
using UnityEngine;
using SmartDebugOverlay.Core;

public class FrameCounterModule : OverlayModuleBase
{
    private int frameCount;

    protected override void OnTick()
    {
        frameCount++;
        UpdateTextData($"Frames: {frameCount}");
    }

    protected override void OnOverlayDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Vector3.zero, 1f);
    }
}
```

---

## 📦 Core Components

### OverlayManager
- Manages update ticks for all registered modules.
- Default update interval: **0.1 seconds** (adjustable).
- Ensures only one instance exists in the scene.

### OverlayModuleBase
- Base class for building custom overlay modules.

Key methods:
```csharp
protected abstract void OnTick(); // Called every update
protected virtual void OnOverlayDrawGizmos() {} // Optional custom gizmo drawing
protected virtual void OnOverlayDrawGizmosSelected() {} // Gizmos when object selected
```

---

## 🧩 Built-in Modules

| Module | Description |
|:------|:------------|
| **FPS Module** | Shows real-time FPS. |
| **Frame Time Module** | Displays frame time in ms. |
| **Memory Module** | Shows total, GC, and GPU memory usage. |
| **GC Button Module** | UI Button to manually trigger Garbage Collection. |
| **Battery Status Module** | Displays battery status (for mobile devices). |
| **Object Tracker Module** | Asynchronously tracks and counts scene objects. |
| **Event Logger Module** | Displays runtime events logged via `SmartLog`. |

---

### 🔎 Object Tracker Module

Track specific object types or custom components with minimal overhead.

- Supports:
  - GameObjects
  - Rigidbodies
  - Colliders
  - ParticleSystems
  - AudioSources
  - MonoBehaviours
  - Custom types by name (e.g., "PlayerController")

```csharp
[SerializeField] private ObjectType objectType = ObjectType.GameObjects;
[SerializeField] private string customTypeName; // for custom types
[SerializeField] private float updateInterval = 60f; // refresh every 60s
```

> ✨ *Scanning is done asynchronously to avoid frame drops, even with thousands of objects.*

---

### 📝 Event Logger Module

Simple runtime event logging with automatic lifetime management.

Log events from anywhere:
```csharp
SmartLog.Log("Enemy defeated!");
```

Example usage:
```csharp
using SmartDebugOverlay.Modules;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void Start()
    {
        SmartLog.Log("Player spawned.");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SmartLog.Log("Player jumped.");
        }
    }
}
```

---

## 📥 Requirements

- Unity **2021.3** or newer (tested on Unity 2021, 2022, 2023, and 2025 beta).

---

# ✅ Why Smart Debug Overlay?

- Extremely simple setup — no complex configuration needed.
- Customizable — create and extend tools with just a few lines of code.
- Designed for indie developers or anyone needing quick, efficient debugging.

---

# 📸 Screenshots
![image](https://github.com/user-attachments/assets/c7319292-5727-4d36-9870-6c516bdf4285)
![image](https://github.com/user-attachments/assets/e75b2b0f-c270-4de8-b94d-94290ef82096)
