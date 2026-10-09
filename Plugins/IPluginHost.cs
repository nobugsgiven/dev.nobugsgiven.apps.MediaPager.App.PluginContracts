namespace MediaPager.App.PluginContracts;

/// <summary>Read-only access to plugin instances that the host has already loaded. Plugins
/// can use this service to cooperate with declared dependencies without taking a dependency
/// on the host's implementation assemblies.</summary>
public interface IPluginHost
{
    IReadOnlyList<IMediaPagerPlugin> Plugins { get; }

    IMediaPagerPlugin? GetPlugin(string id);

    TPlugin? GetPlugin<TPlugin>() where TPlugin : class;

    IReadOnlyList<TPlugin> GetPlugins<TPlugin>() where TPlugin : class;
}
