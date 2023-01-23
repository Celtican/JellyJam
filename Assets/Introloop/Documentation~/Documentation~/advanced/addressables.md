# Integration with Addressable Asset System

Introloop provides light integration with Unity's [Addressable Asset System](https://docs.unity3d.com/Packages/com.unity.addressables@latest) with conditional define in its Assembly Definition (`.asmdef`).

`HAS_AAS` is defined once the package detects that you have installed the Addressables package.

It enables...

## Pre-made addressable `IntroloopAudio`

```csharp
#if HAS_AAS
using System;
using UnityEngine.AddressableAssets;

namespace E7.Introloop
{
    /// <summary>
    /// Serialize an addressable asset reference to <see cref="IntroloopAudio"/>.
    /// You can load this asynchronously before using the result with <see cref="IntroloopPlayer"/>.
    /// </summary>
    [Serializable]
    public class AssetReferenceIntroloopAudio : AssetReferenceT<IntroloopAudio>
    {
        public AssetReferenceIntroloopAudio(string guid) : base(guid)
        {
        }
    }
}
#endif
```

By declaring `[SerializeField] private AssetReferenceIntroloopAudio myMusic` in your code, you can serialize Addressables reference instead of regular reference which won't drag that `IntroloopAudio` "inside" to be included in your project. Instead, you use Addressables API to request the bundle containing this asset at runtime to asynchronously load it.

You can do this on your own for any kind of address by subclassing `AssetReferenceT<YourClass>` (Due to Unity's limitation of not being able to serialize a class if it still has generic tag on it). However I think if the package provide this as well it can make your project a bit cleaner.

Music is a good use case for Addressables, since music takes up quite a big chunk of your build. If your game has a live server serving it, asset bundles containing music can be downloaded lazily when it is going to be played, especially for rare scenes that requires special music.