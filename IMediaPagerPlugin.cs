// Base plugin contract: identity only. Capabilities are opt-in interfaces
// (IStreamSourcePlugin, IMetadataProviderPlugin, ISearchProviderPlugin,
// ISubtitleProviderPlugin) a single plugin class may implement any subset of.
namespace MediaPager.App.PluginContracts;

/// <summary>Stable identity for a plugin instance. One instance per implementation type.</summary>
public sealed record PluginDescriptor(
    string Id,                      // reverse-dns id, e.g. "mediapager.metadata.tmdb" / "community.overcast-subtitles"
    string Name,
    string Version,                 // semver of the plugin itself
    string? Author = null,
    string? Description = null);

public interface IMediaPagerPlugin
{
    PluginDescriptor Descriptor { get; }
}