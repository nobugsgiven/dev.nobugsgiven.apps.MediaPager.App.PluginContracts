namespace MediaPager.App.PluginContracts;

/// <summary>Marker capability for a plugin that provides a contract assembly to other plugins.
/// The host treats it as an ordinary installable plugin; the interface it exports remains
/// outside the host SDK.</summary>
public interface IPluginInterface : IMediaPagerPlugin
{
}
