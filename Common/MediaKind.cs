namespace MediaPager.App.PluginContracts;

/// <summary>The six content kinds. Mirrors the CatalogType/MediaType lookup families.</summary>
public enum MediaKind
{
    Movie,
    Tv,
    Music,
    Podcast,
    Audiobook,
    Book,
}

public static class MediaKindExtensions
{
    public static string ToSlug(this MediaKind kind) => kind switch
    {
        MediaKind.Movie => "movie",
        MediaKind.Tv => "tv",
        MediaKind.Music => "music",
        MediaKind.Podcast => "podcast",
        MediaKind.Audiobook => "audiobook",
        MediaKind.Book => "book",
        _ => throw new ArgumentOutOfRangeException(nameof(kind), kind, "Unknown media kind."),
    };

    public static string ToCatalogTypeSlug(this MediaKind kind) => kind switch
    {
        MediaKind.Movie => "movies",
        MediaKind.Tv => "tv-shows",
        MediaKind.Music => "music",
        MediaKind.Podcast => "podcasts",
        MediaKind.Audiobook => "audiobooks",
        MediaKind.Book => "books",
        _ => throw new ArgumentOutOfRangeException(nameof(kind), kind, "Unknown media kind."),
    };

    public static MediaKind FromSlug(string slug) => slug.Trim().ToLowerInvariant() switch
    {
        "movie" or "movies" => MediaKind.Movie,
        "tv" or "tv-shows" or "tvshows" => MediaKind.Tv,
        "music" => MediaKind.Music,
        "podcast" or "podcasts" => MediaKind.Podcast,
        "audiobook" or "audiobooks" => MediaKind.Audiobook,
        "book" or "books" => MediaKind.Book,
        _ => throw new ArgumentOutOfRangeException(nameof(slug), slug, "Unknown media kind slug."),
    };
}

/// <summary>A page of results. Null TotalPages when the source does not expose pagination.</summary>
public sealed record Paged<T>(IReadOnlyList<T> Items, int Page = 1, int? TotalPages = null);
