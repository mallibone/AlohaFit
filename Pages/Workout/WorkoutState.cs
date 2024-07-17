using System;
using System.Collections.Generic;
using MauiReactor;

namespace AlohaFit.Pages;

internal class WorkoutState
{
    public DateTimeOffset StartTime { get; set; }
    public TimeSpan Duration { get; set; }
    public int Rounds { get; set; }
    public bool IsRunning { get; set; }
    public IReadOnlyCollection<DurationOption> DurationOptions { get; set; } = Array.Empty<DurationOption>();
    public int SelectedDurationIndex { get; set; }
    public bool WorkoutCompleted { get; set; }
    public TimeSpan Remaining { get; set; }
    public string DurationLabel { get; set; } = "As many rounds as possible in";
}