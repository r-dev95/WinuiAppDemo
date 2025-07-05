using System.Text.Json.Serialization;

using WinuiAppDemo.Models;

namespace WinuiAppDemo
{
    /// <summary>
    /// Provides functionality for serializing and deserializing the application settings.
    /// </summary>
    [JsonSerializable(typeof(SettingsApp))]
    public partial class AppJsonContext : JsonSerializerContext
    {
    }
}
