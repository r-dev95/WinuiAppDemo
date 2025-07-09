using System.Text.Json;
using System.Text.Json.Serialization;

using WinuiAppDemo.Models;

namespace WinuiAppDemo;

/// <summary>
/// Provides functionality for serializing and deserializing the application settings.
/// </summary>
[JsonSourceGenerationOptions(WriteIndented = true, GenerationMode = JsonSourceGenerationMode.Metadata)]
[JsonSerializable(typeof(UserSettings))]
public partial class AppJsonContext : JsonSerializerContext
{
    /// <summary>
    /// Gets a new instance of <see cref="AppJsonContext"/> with indented formatting.
    /// </summary>
    public static AppJsonContext Indented { get; } = new (new JsonSerializerOptions
    {
        WriteIndented = true,
    });

    /// <summary>
    /// Gets a new instance of <see cref="AppJsonContext"/> with compact formatting.
    /// </summary>
    public static AppJsonContext Compact { get; } = new (new JsonSerializerOptions
    {
        WriteIndented = false,
    });
}
