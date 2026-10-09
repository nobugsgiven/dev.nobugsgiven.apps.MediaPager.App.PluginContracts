namespace MediaPager.App.PluginContracts;

/// <summary>Type of a data-driven setting field; the settings UI renders generically from these.</summary>
public enum PluginSettingType
{
    String,
    Password,
    Boolean,
    Catalog,
}

/// <summary>One entry in a plugin's settings schema. Keys live namespaced under <c>plugins.{pluginKey}.{Key}</c>.</summary>
public sealed record PluginSettingDefinition(
    string Key,
    string Label,
    PluginSettingType Type = PluginSettingType.String,
    bool Required = false,
    bool Secret = false,
    string? Default = null,
    string? CatalogTypeSlug = null);

/// <summary>Optional capability: declare the settings the plugin reads, so the settings UI
/// (and the deployer's validation) is data-driven instead of per-provider hard-coded.</summary>
public interface IPluginSettingsSchema
{
    IReadOnlyList<PluginSettingDefinition> Settings { get; }
}
