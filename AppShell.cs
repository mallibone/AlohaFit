using MauiReactor;

using AlohaFit.Pages;
using Microsoft.Maui.ApplicationModel;

namespace AlohaFit;

class AppShell : Component
{

    protected override void OnMounted()
    {
        if (MauiControls.Application.Current is not null) MauiControls.Application.Current.UserAppTheme = AppTheme.Dark;
        
        Routing.RegisterRoute<WorkoutPage>(nameof(WorkoutPage));
        Routing.RegisterRoute<EmomWorkoutPage>(nameof(EmomWorkoutPage));
        base.OnMounted();
    }

    public override VisualNode Render() => Shell(ShellContent().Title("").RenderContent(() => new MainPage()));
}

public static class NavigationRoutes
{
    public static string WorkoutPage => $"{nameof(WorkoutPage)}";
    public static string EmomWorkoutPage => $"{nameof(EmomWorkoutPage)}";
}

