namespace MediaPager.App.PluginContracts;

public enum SubtitleFormat
{
    Srt,
    Vtt,
}

public sealed record SubtitleRequest(
    string Language,
    string? ImdbId = null,
    string? TmdbId = null,
    string? Query = null,
    int? Season = null,
    int? Episode = null,
    bool HearingImpairedOnly = false);

public sealed record SubtitleHit(
    string FileId,
    string Language,
    string? FileName = null,
    string? Release = null,
    bool HearingImpaired = false,
    long Popularity = 0);

/// <summary>FileId is opaque (provider-specific); only the same provider can fetch it back.</summary>
public sealed record SubtitleFetchRequest(string FileId);

/// <summary>Native content; the core converts Srt to WebVTT so browser tracks work.</summary>
public sealed record SubtitleDocument(string Content, SubtitleFormat Format);

public interface ISubtitleProviderPlugin : IMediaPagerPlugin
{
    Task<IReadOnlyList<SubtitleHit>> SearchAsync(SubtitleRequest request, CancellationToken cancellationToken);

    Task<SubtitleDocument?> FetchAsync(SubtitleFetchRequest request, CancellationToken cancellationToken);
}
