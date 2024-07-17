using System;

namespace AlohaFit.Pages;

public record DurationOption(TimeSpan Duration)
{
    public override string ToString()
    {
        return Duration.TotalMinutes switch
        {
            < 1 => $"{Duration:mm\\:ss} minutes",
            1 => $"{Duration.TotalMinutes} minute",
            _ => Duration.Seconds > 0 ? $"{Duration:mm\\:ss} minutes" : $"{Duration.TotalMinutes} minutes"
        };
        // return Duration.TotalMinutes > 1 ? $"{Duration.TotalMinutes} minutes" : $"{Duration.TotalMinutes} minute";
    }
}