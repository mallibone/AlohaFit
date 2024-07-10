using System;

namespace AlohaFit.Pages;

public record DurationOption(TimeSpan Duration)
{
    public override string ToString()
    {
        return Duration.TotalMinutes > 1 ? $"{Duration.TotalMinutes} minutes" : $"{Duration.TotalMinutes} minute";
    }
}