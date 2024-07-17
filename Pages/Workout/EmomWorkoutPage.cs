using System;
using System.Collections.Generic;
using System.Linq;
using MauiReactor;

namespace AlohaFit.Pages;

public class EmomWorkoutPage : Component<EmomWorkoutState, WorkoutParameters>
{
    protected override void OnMounted()
    {
        State.DurationOptions = new List<DurationOption>
        {
            new(TimeSpan.FromSeconds(30)),
            new(TimeSpan.FromSeconds(45)),
            new(TimeSpan.FromSeconds(60)),
            new(TimeSpan.FromSeconds(75)),
            new(TimeSpan.FromSeconds(90)),
            new(TimeSpan.FromSeconds(120)),
            new(TimeSpan.FromSeconds(150)),
            new(TimeSpan.FromSeconds(180)),
            new(TimeSpan.FromSeconds(210)),
            new(TimeSpan.FromSeconds(240)),
            new(TimeSpan.FromSeconds(300)),
            new(TimeSpan.FromSeconds(360)),
            new(TimeSpan.FromSeconds(420)),
            new(TimeSpan.FromSeconds(480)),
            new(TimeSpan.FromSeconds(540)),
            new(TimeSpan.FromSeconds(600)),
            new(TimeSpan.FromSeconds(660)),
            new(TimeSpan.FromSeconds(720)),
            new(TimeSpan.FromSeconds(780)),
            new(TimeSpan.FromSeconds(840)),
            new(TimeSpan.FromSeconds(900)),
        };
        base.OnMounted();
    }

    public override VisualNode Render()
        => ContentPage(
            Grid(
                !State.IsRunning
                    ? ConfigurationView()
                    : RunningView(),
            Timer()
                .Interval(1000)
                .IsEnabled(State.IsRunning)
                .OnTick(TimerCallback)
            )
        ).Title($"{Props.SelectedWorkoutMode}");
    
    private VisualNode RunningView() =>
        Grid("*,Auto", "*",
            VStack(
                    Label("Time Remaining")
                        .FontSize(18)
                        .Style("")
                        .HCenter(),
                    Label(State.Remaining.ToString(@"mm\:ss"))
                        .FontSize(48)
                        .HCenter()
                )
                .VCenter()
                .Spacing(18),
            Components.PrimaryButton("Cancel Workout", Colors.DarkOrange, StopWorkout)
                .HCenter().GridRow(1)
        );

    private VisualNode ConfigurationView() =>
        Grid("*,Auto", "*",
            VStack(
                    Label(State.DurationLabel)
                        .Style("H3")
                        .HCenter(),
                    HStack(
                            Picker()
                                .Title("Select Duration")
                                .SelectedIndex(State.SelectedDurationIndex)
                                .ItemsSource(State.DurationOptions.Select(d => d.ToString()).ToList())
                                .Style("SubHeadline")
                                .OnSelectedIndexChanged(i => State.SelectedDurationIndex = i)
                        )
                        .Spacing(4).HCenter()
                )
                .Spacing(18)
                .VCenter(),
            Components.PrimaryButton("Start Workout", Colors.DarkOrange, StartWorkout)
                .GridRow(1));
    
    private void StartWorkout()
    {
        SetState(s =>
        {
            s.IsRunning = true;
            s.StartTime = DateTimeOffset.Now;
            s.Duration = TimeSpan.FromMinutes(s.SelectedDurationIndex + 1);
            var elapsed = DateTimeOffset.Now - State.StartTime;
            s.Remaining = State.Duration - elapsed;
        });
    }

    private void TimerCallback()
    {
        var elapsed = DateTimeOffset.Now - State.StartTime;
        var remaining = State.Duration - elapsed;
        if (remaining <= TimeSpan.Zero)
        {
            SetState(s =>
            {
                s.IsRunning = false;
                s.WorkoutCompleted = true;
                s.Duration = TimeSpan.Zero;
                s.WorkoutCompleted = true;
            });
            return;
        }

        SetState(s => s.Remaining = remaining);
    }

    private void StopWorkout()
    {
        SetState(s =>
        {
            s.IsRunning = false;
            s.Duration = TimeSpan.Zero;
            s.Rounds = 0;

        });
    }
}

public class EmomWorkoutState
{
    public DateTimeOffset StartTime { get; set; }
    public TimeSpan Duration { get; set; }
    public int Rounds { get; set; }
    public bool IsRunning { get; set; }
    public IReadOnlyCollection<DurationOption> DurationOptions { get; set; } = Array.Empty<DurationOption>();
    public int SelectedDurationIndex { get; set; }
    public bool WorkoutCompleted { get; set; }
    public TimeSpan Remaining { get; set; }
    public string DurationLabel { get; set; } = "Every";
    public string RoundLabel { get; set; } = "For";
}
