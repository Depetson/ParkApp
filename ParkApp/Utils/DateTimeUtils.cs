namespace ParkApp.Utils;

public static class DateTimeUtils
{
    /// <summary>
    /// Truncates to specific resolution 
    /// </summary>
    /// <param name="date">DateTiem to truncate</param>
    /// <param name="resolution">Resolution e.g. TimeSpan.TicksPerSecond for truncate to seconds</param>
    public static DateTime Truncate(this DateTime date, long resolution)
    {
        return new DateTime(date.Ticks - date.Ticks % resolution, date.Kind);
    }

    /// <summary>
    /// Removes utc info
    /// </summary>
    /// <param name="date">DateTime object to remove from</param>
    public static DateTime ClearUtcInfo(this DateTime date)
    {
        return new DateTime(date.Ticks);
    }

    /// <summary>
    /// Truncates to specific resolution and removes utc info
    /// </summary>
    /// <param name="date">DateTime to truncate</param>
    /// <param name="resolution">Resolution e.g. TimeSpan.TicksPerSecond for truncate to seconds</param>
    public static DateTime FormatDate(this DateTime date, long resolution)
    {
        return date.ClearUtcInfo().Truncate(resolution);
    }
}
