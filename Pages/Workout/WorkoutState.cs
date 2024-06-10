using System;
using System.Collections.Generic;

namespace AlohaFit.Pages;

internal class WorkoutState
{
    public DateTimeOffset StartTime { get; set; }
    public TimeSpan Duration { get; set; }
    public int Rounds { get; set; }
    public bool IsRunning { get; set; }
    public IReadOnlyCollection<DurationOption> DurationOptions { get; set; }
    public int SelectedDurationIndex { get; set; }
    public bool WorkoutCompleted { get; set; }
    public TimeSpan Remaining { get; set; }
}