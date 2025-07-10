using System;

namespace WinuiAppDemo.Services.Interfaces;

/// <summary>
/// Provides functionality for retrieving the current time.
/// </summary>
public interface IClockService
{
    /// <summary>
    /// Provides the current time.
    /// </summary>
    /// <returns>The current time.</returns>
    DateTime GetCurrentTime();
}
