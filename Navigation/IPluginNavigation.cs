namespace MediaPager.App.PluginContracts;

/// <summary>
/// A plugin's request for its own entry in the Settings left-nav. When <see cref="UiPath"/> is
/// null the host renders the plugin's declared <see cref="IPluginSettingsSchema"/> (the default
/// panel); when set, the host embeds the plugin's own UI from <c>/plugins/{key}/ui/{UiPath}</c>.
/// </summary>
public sealed record PluginSettingsNav(
    string Label,
    string? Icon = null,
    string? UiPath = null);

/// <summary>An entry a plugin contributes to the main left-nav. <see cref="Path"/> is relative
/// to the host's generic plugin route (<c>/plugin/{key}/{Path}</c>), which embeds the plugin's
/// UI from <c>/plugins/{key}/ui/{Path}</c>.</summary>
public sealed record PluginMenuEntry(
    string Label,
    string Icon,
    string Path);

/// <summary>
/// Optional capability: declares navigation the host should surface — a Settings left-nav entry
/// and/or main-menu entries. Everything is opt-in; a plugin with neither stays reachable the
/// ordinary way (settings gear). The host owns routing and renders plugin UI in a sandboxed frame.
/// </summary>
public interface IPluginNavigation : IMediaPagerPlugin
{
    PluginSettingsNav? SettingsNav => null;

    IReadOnlyList<PluginMenuEntry> MainNav => [];
}
