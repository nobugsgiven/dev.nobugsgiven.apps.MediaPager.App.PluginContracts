# MediaPager.App.PluginContracts

The **plugin SDK** for MediaPager — interfaces and DTOs only, **zero package dependencies**.
Plugins bind to this clean seam and never touch the host (`MediaPager.App.Core`) or any web
framework, so a plugin compiled against this assembly runs inside any MediaPager host.

## What it is

A single plugin class implements any mix of capability interfaces. The host discovers the
class via `IMediaPagerPlugin` and surfaces each capability it supports through one registry.

```csharp
public interface IMediaPagerPlugin
{
    PluginDescriptor Descriptor { get; }   // id, name, version, author, description
}
```

## Capability interfaces

| Interface | Purpose |
|---|---|
| `IStreamProviderPlugin` | Nav `Sources` + browse/details + `ResolveAsync` → a raw upstream stream URI. The core validates it (SSRF guard) and mints the proxied playback session. |
| `IMetadataProviderPlugin` | Id-based details/enrichment for scanning/updating existing library data. |
| `ISearchProviderPlugin` | Fuzzy unified-search hits contributed into the type-ahead. |
| `ISubtitleProviderPlugin` | Subtitle search + fetch; returns **native** content (SRT/VTT). The host converts SRT→WebVTT for browser tracks. |
| `IPluginSettingsSchema` | *(optional)* Declares the settings the plugin reads so the settings UI is data-driven. |

## Supporting types

- `IPluginSettingsStore` — core-hosted, namespaced settings under
  `plugins.<pluginKey>.<name>`. Not whitelisted; read/write/delete freely.
- `PluginSettingDefinition` — one data-driven setting field (`String`/`Password`, `Required`,
  `Secret`, `Default`).
- `MediaKind` — the six content kinds (`Movie`, `Tv`, `Music`, `Podcast`, `Audiobook`, `Book`).
- `TitleDetails` / `BrowseItem` / `SeasonInfo` / `EpisodeInfo` / `CreditRole` / `EnrichmentData`
  — the content DTOs.
- `Paged<T>` — a page of results (`Items`, `Page`, optional `TotalPages`).

## Conventions

- "Not available" is a `null` return (e.g. `ResolveAsync`, `GetDetailsAsync`), never an
  exception for ordinary misses — a provider fails clean.
- Plugins reference **Contracts only**; the host references Contracts; hosts reference Core
  (+ plugins at runtime).
- The SDK stays dependency-free: shared infrastructure abstractions (e.g. a browser engine)
  are deliberately deferred — a plugin owns its own dependencies.

## Example skeleton

```csharp
public sealed class MyPlugin : IMediaPagerPlugin, IPluginSettingsSchema
{
    public PluginDescriptor Descriptor { get; } = new(
        Id: "mediapager.mine",
        Name: "My Plugin",
        Version: "0.1.0",
        Author: "Me",
        Description: "Does a thing.");

    public IReadOnlyList<PluginSettingDefinition> Settings { get; } =
    [
        new("apiKey", "API key", PluginSettingType.Password, Required: true, Secret: true),
    ];
}
```
