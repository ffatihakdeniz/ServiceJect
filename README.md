# 📦 ServiceJect → Simple Dependency Injection System for Unity

**Geliştiren:** Fatih AKDENİZ  
🔗 GitHub: [ffatihakdeniz](https://github.com/ffatihakdeniz)

---

## 🚀 Nedir?

`ServiceJect`, Unity için geliştirilmiş sade, hızlı ve anlaşılır bir **Dependency Injection (DI)** sistemidir.  
Zenject gibi kompleks yapılar yerine daha yalın bir yapı tercih edenler için geliştirilmiştir.  
Zenject + Service Locator desenlerinin birleştirilmesiyle  ServiceJect oluşmuştur.

---

## 🎯 Temel Özellikler

- ✔️ Attribute ile otomatik servis kaydı (`[ServiceJectRegister]`)
- ✔️ Field/property injection desteği (`[GetService]`)
- ✔️ Sahnede olmayan servisleri manuel kaydetme desteği
- ✔️ Sahnedeki MonoBehaviour'lara otomatik inject
- ✔️ `InjectableMonoBehaviour` ile sadece belirli bileşenleri hedefleme
- ✔️ `ServiceJectDebugWindow` ile editör arayüzünden servisleri görüntüleme

---

## 🔧 Kurulum

1. ServiceJect klasörünü indir.
2. Unity projenin içine çıkar.
3. Sahnene bir `GameObject` ekleyip `ServiceJectInitializer` component’ini bağla.

---

## 🧪 Kullanım Örnekleri

### Otomatik servis kaydı:
```csharp
[ServiceJectRegister]
public class AudioManager
{
    public void Play(string sound) => Debug.Log($"Playing: {sound}");
}
```

### Otomatik inject:
```csharp
public class GameController : MonoBehaviour
{
    [GetService] private AudioManager _audio;

    void Start() => _audio.Play("MenuClick");
}
```

### Sadece özel bileşenleri inject etmek için:
```csharp
public class MyClass : InjectableMonoBehaviour
{
    [GetService] private AudioManager _audio;
}
```

### Manuel servis kaydı:
```csharp
ServiceJect.RegisterService<Logger>(new Logger());
var logger = ServiceJect.GetService<Logger>();
```

---

## 🧰 Editor Debug Aracı

Menü: `Tools > ServiceJect > Debug`

- Kayıtlı servisleri listeler
- Sahnedeki inject edilmiş bileşenleri görselleştirir

---

## 📄 Lisans

MIT Lisansı.  
Dilediğiniz gibi kullanabilir, değiştirebilir ve dağıtabilirsiniz.

---

## ✍️ Geliştirici

**Fatih AKDENİZ**  
GitHub: [ffatihakdeniz](https://github.com/ffatihakdeniz)  
“Karmaşık sistemlere gerek kalmadan sade DI yönetimi. :)”

<p align="center">
  <img src="https://raw.githubusercontent.com/ffatihakdeniz/ServiceJect/main/ServiceJect_Banner.png" width="600"/>
</p>

