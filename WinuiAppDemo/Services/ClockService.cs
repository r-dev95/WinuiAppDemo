using System;

using NLog;

using WinuiAppDemo.Services.Interfaces;

namespace WinuiAppDemo.Services;

/// <inheritdoc />
public class ClockService : IClockService
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    /// <inheritdoc />
    public DateTime GetCurrentTime() => DateTime.Now;
}
