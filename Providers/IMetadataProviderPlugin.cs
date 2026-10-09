namespace MediaPager.App.PluginContracts;

/// <summary>
/// Provides canonical metadata by id, for scanning library folders, enriching library records,
/// and backfilling detail sheets. Distinct from ISearchProviderPlugin (fuzzy discovery).
/// </summary>
public interface IMetadataProviderPlugin : IMediaPagerPlugin
{
    /// <summary>Whether this provider covers the given content kind.</summary>
    bool Handles(MediaKind kind);

    Task<TitleDetails?> GetDetailsAsync(MediaKind kind, string externalId, CancellationToken cancellationToken);

    /// <summary>Canonical metadata for creating/backfilling a library entry.</summary>
    Task<EnrichmentData?> EnrichAsync(string externalId, MediaKind kind, CancellationToken cancellationToken);
}
