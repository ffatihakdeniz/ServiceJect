
<p align="center">
  <img src="https://github.com/ffatihakdeniz/ServiceJect/blob/main/ServiceJect_Banner.png" width="600"/>
</p>

---

# 📦 ServiceJect – Simple & Powerful Dependency Injection for Unity

**Created by:** Fatih AKDENİZ  
🔗 GitHub: [@ffatihakdeniz](https://github.com/ffatihakdeniz)

---

> ⚡ A lightweight, reflection-based dependency injection (DI) tool for Unity.  
> Designed to combine the power of **Service Locator** and **Zenject-like** injection without the complexity.

---

## 🚀 Features

- 🧩 Attribute-based service registration (`[ServiceJectRegister]`)
- 🔌 Auto-injection for fields and properties (`[GetService]`)
- 🧠 Auto-register and inject `MonoBehaviour` components in the scene
- 🧱 Restrict injection to specific components via `InjectableMonoBehaviour`
- 🔧 Manual `RegisterService` and `GetService` methods available
- 🧰 Built-in **Unity Editor Debug Window** for live inspection

---

## 📦 Installation

1. Download or clone the repository:
```bash
git clone https://github.com/ffatihakdeniz/ServiceJect.git
```

2. Place the contents into your Unity project, e.g.:
```
Assets/Scripts/Patterns/ServiceJect/
```

3. Be sure to move the `Editor/ServiceJectDebugWindow.cs` file into an `Editor` folder.

---

## 🔧 Setup

- Create an empty GameObject in your scene.
- Add the `ServiceJectInitializer` component to it.

### Component Settings:
| Option | Description |
|--------|-------------|
| **Use Only Service Ject MonoBehaviour** | Only injects components that inherit from `InjectableMonoBehaviour` |
| **Use Inactive GameObject** | Also includes inactive objects in injection |

---

## 🧪 Examples

### 1. Auto-register a class:
```csharp
[ServiceJectRegister]
public class AudioManager
{
    public void Play(string sound) => Debug.Log($"Playing: {sound}");
}
```

### 2. Auto-inject using [GetService]:
```csharp
public class GameController : MonoBehaviour
{
    [GetService]
    private AudioManager _audioManager;

    private void Start()
    {
        _audioManager.Play("Click");
    }
}
```

### 3. Restrict injection to specific components:
```csharp
public class MyInjectedComponent : InjectableMonoBehaviour
{
    [GetService]
    private AudioManager _audio;
}
```

### 4. Manual service registration:
```csharp
ServiceJect.RegisterService<Logger>(new Logger());
var logger = ServiceJect.GetService<Logger>();
logger.Log("Manually registered and retrieved.");
```

---

## 🧰 Debug Tool

Open it from Unity menu:  
**`Tools > ServiceJect > Debug`**

- Lists all registered services
- Helps visualize what's injected and when

---

## 📄 License

MIT License  
Feel free to use, modify, and distribute.

---

## 👨‍💻 Author

**Fatih AKDENİZ**  
📍 GitHub: [@ffatihakdeniz](https://github.com/ffatihakdeniz)

> "Powerful DI doesn’t need to be complicated."
