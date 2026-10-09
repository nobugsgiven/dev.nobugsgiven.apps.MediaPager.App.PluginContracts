namespace MediaPager.App.PluginContracts;

/// <summary>
/// Optional capability: a plugin that can deliver email (password resets, invites).
/// The host picks the active one via the Email:Provider runtime setting and falls back
/// to the first configured provider, then the first installed one. With no email plugin
/// loaded the host simply cannot send mail — auth flows degrade to "not configured".
/// </summary>
public interface IEmailProviderPlugin : IMediaPagerPlugin
{
    /// <summary>Whether the plugin has everything it needs to actually send.</summary>
    Task<bool> IsConfiguredAsync(CancellationToken cancellationToken = default);

    /// <summary>Send one message. Returns false on provider-side failure (already logged by the plugin).</summary>
    Task<bool> SendAsync(string recipient, string subject, string text, string html,
        CancellationToken cancellationToken = default);
}
