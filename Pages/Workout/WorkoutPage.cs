using System;
using System.Linq;
using System.Timers;
using AlohaFit.Types;
using MauiReactor;
using Timer = System.Timers.Timer;

namespace AlohaFit.Pages;

class WorkoutPage : Component<WorkoutState, WorkoutParameters>
{
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
                s.Duration = TimeSpan.Zero;
                s.Rounds = 0;
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
            s.Remaining = TimeSpan.Zero;
            s.Rounds = 0;
        });
    }
}

public class WorkoutParameters
{
    public WorkoutModes SelectedWorkoutMode { get; set; }
}