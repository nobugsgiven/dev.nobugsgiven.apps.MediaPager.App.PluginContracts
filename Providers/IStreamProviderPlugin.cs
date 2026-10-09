namespace MediaPager.App.PluginContracts;

/// <summary>A navigable entry this source contributes to the left-nav "Stream" node
/// (and its data-driven sub-tabs/grids).</summary>
public sealed record SourceDescriptor(
    string Key,
    string Name,
    MediaKind Kind,
    string? Icon = null,
    bool CustomUi = false,
    string? UiUrl = null);

public sealed record StreamResolveRequest(
    string SourceKey,
    string ExternalId,
    MediaKind Kind,
    int? Season = null,
    int? Episode = null);

/// <summary>
/// An upstream stream URL. The core validates it (SSRF guard: HTTPS + public IP only),
/// mints a short-lived session, and proxies playlists/segments anonymously. A plugin
/// never produces session ids or proxy URLs — only the raw upstream URI.
/// </summary>
public sealed record StreamResult(Uri UpstreamUri);

/// <summary>Where a stream provider gets its content from. <see cref="Online"/>
/// providers power the nav "Stream" node (browse the provider's catalog, resolve
/// playback through an upstream service); <see cref="Local"/> providers serve local media
/// and stay behind the user's catalogs instead of activating Stream.</summary>
public enum StreamProviderMode
{
    Local,
    Online,
}

/// <summary>
/// A stream provider: identifies the titles it can play (nav "Stream" entries + feeds) and
/// resolves playback to an upstream URL. Returns null from ResolveAsync when unavailable.
/// Conventions: the provider act is ResolveAsync; Browse/GetDetails fill in the provider's
/// catalog (which the provider may hand off to an injected metadata provider).
/// </summary>
public interface IStreamProviderPlugin : IMediaPagerPlugin
{
    /// <summary>Online providers activate the Stream nav entry; local ones obey the
    /// catalogs. Defaults to Online so plugins written before this existed keep working.</summary>
    StreamProviderMode Mode => StreamProviderMode.Online;

    /// <summary>Catalog media types this provider handles. Online providers default to the
    /// kinds declared by their sources; local providers may support catalog types without
    /// contributing browsable sources.</summary>
    IReadOnlyList<MediaKind> SupportedKinds => Sources.Select(source => source.Kind).Distinct().ToArray();

    IReadOnlyList<SourceDescriptor> Sources { get; }

    Task<Paged<BrowseItem>> BrowseAsync(string sourceKey, string? query, int page, CancellationToken cancellationToken);

    Task<TitleDetails?> GetDetailsAsync(string sourceKey, string externalId, CancellationToken cancellationToken);

    Task<StreamResult?> ResolveAsync(StreamResolveRequest request, CancellationToken cancellationToken);
}
