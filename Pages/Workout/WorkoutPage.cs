using System;
using System.Linq;
using System.Timers;
using AlohaFit.Types;
using MauiReactor;
using Timer = System.Timers.Timer;

namespace AlohaFit.Pages;

class WorkoutPage : Component<WorkoutState, WorkoutParameters>
{
    private Timer? _timer = new Timer(TimeSpan.FromSeconds(1));
    protected override void OnMounted()
    {
        State.DurationOptions =
            Enumerable.Range(1, 100).Select(i => new DurationOption(TimeSpan.FromMinutes(i))).ToArray();
        base.OnMounted();
    }

    public override VisualNode Render()
        => ContentPage(
            Grid(
                !State.IsRunning
                    ? ConfigurationView()
                    : RunningView()
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
                    Label("As many rounds as possible in")
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
        SetState(s => s.IsRunning = true);
        SetState(s => s.StartTime = DateTimeOffset.Now);
        SetState(s => s.Duration = TimeSpan.FromMinutes(s.SelectedDurationIndex+1));
        var elapsed = DateTimeOffset.Now - State.StartTime;
        SetState(s => s.Remaining = State.Duration - elapsed);
        _timer.Elapsed += TimerCallback;
        _timer.AutoReset = true;
        _timer.Interval = 1000; // 1 second
        _timer.Start();
    }

    private void TimerCallback(object? sender, ElapsedEventArgs e)
    {
        var elapsed = DateTimeOffset.Now - State.StartTime;
        var remaining = State.Duration - elapsed;
        if (remaining <= TimeSpan.Zero)
        {
            _timer.Stop();
            SetState(s => s.IsRunning = false);
            SetState(s => s.Duration = TimeSpan.Zero);
            SetState(s => s.Rounds = 0);
            SetState(s => s.WorkoutCompleted = true);
            return;
        }

        SetState(s => s.Remaining = remaining);
    }

    private void StopWorkout()
    {
        SetState(s => s.IsRunning = false);
        SetState(s => s.Duration = TimeSpan.Zero);
        SetState(s => s.Remaining = TimeSpan.Zero);
        SetState(s => s.Rounds = 0);
        _timer.Stop();
    }
}

public class WorkoutParameters
{
    public WorkoutModes SelectedWorkoutMode { get; set; }
}