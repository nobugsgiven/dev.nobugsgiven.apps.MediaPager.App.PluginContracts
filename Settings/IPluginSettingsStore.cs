namespace MediaPager.App.PluginContracts;

/// <summary>
/// Core-hosted settings store, namespaced per plugin. Canonical source is the runtime
/// settings DB keyed <c>plugins.{pluginKey}.{name}</c>; app configuration is the fallback.
/// Unlike IRuntimeSettings, plugin keys are not whitelisted and write/delete freely.
/// </summary>
public interface IPluginSettingsStore
{
    Task<string?> GetAsync(string pluginKey, string name, CancellationToken cancellationToken = default);

    /// <summary>Pass a null/whitespace value to delete the stored setting.</summary>
    Task SetAsync(string pluginKey, string name, string? value, CancellationToken cancellationToken = default);

    Task<IReadOnlyDictionary<string, string>> GetAllAsync(string pluginKey, CancellationToken cancellationToken = default);
}