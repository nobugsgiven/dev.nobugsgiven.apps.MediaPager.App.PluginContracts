namespace MediaPager.App.PluginContracts;

/// <summary>Severity of a plugin-raised notification, mirroring the host notifications panel.</summary>
public enum PluginNotificationLevel
{
    Info,
    Success,
    Warning,
    Error,
}

/// <summary>A point-in-time message a plugin raises into the shared notifications panel.</summary>
public sealed record PluginNotification(
    string Id,
    string PluginId,
    string Title,
    string? Message,
    PluginNotificationLevel Level)
{
    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
}

/// <summary>Why a job stopped.</summary>
public enum PluginJobState
{
    Running,
    Completed,
    Failed,
    Cancelled,
}

/// <summary>A snapshot of one plugin-declared job, surfaced to the UI so the notifications
/// panel can monitor progress. <see cref="Progress"/> is 0..1 (null = indeterminate).</summary>
public sealed record PluginJobInfo(
    string Id,
    string PluginId,
    string Title,
    string? Status,
    double? Progress,
    PluginJobState State);

/// <summary>A cancellable handle to a long-running job a plugin started. The plugin drives it;
/// the host only observes and can request cancellation.</summary>
public interface IPluginJob
{
    string Id { get; }

    bool IsCancellationRequested { get; }

    CancellationToken CancellationToken { get; }

    void Report(double progress, string? status = null);

    void Complete(string? status = null);

    void Fail(string status);
}

/// <summary>
/// Host-provided activity surface, injected into plugins that need to notify the user or report
/// long-running progress (for example, file processing or a refresh task). The host owns the
/// pool and notifications panel; the plugin never renders UI for it.
/// </summary>
public interface IPluginActivity
{
    /// <summary>Raise a message into the shared notifications panel.</summary>
    void Notify(string title, string? message = null, PluginNotificationLevel level = PluginNotificationLevel.Info);

    /// <summary>Start a long-running job and return its handle so the plugin can report progress.</summary>
    IPluginJob BeginJob(string title);

    IReadOnlyList<PluginJobInfo> Jobs { get; }

    IReadOnlyList<PluginNotification> Notifications { get; }
}
